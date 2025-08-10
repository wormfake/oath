using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotmail_Get_Oauth2
{
    public static class QuanLyProxy
    {
        private static readonly ConcurrentDictionary<string, HashSet<int>> ProxyTaskMap = new ConcurrentDictionary<string, HashSet<int>>();

        // Lưu trữ IP proxy của từng key proxy
        private static readonly ConcurrentDictionary<string, string> ProxyIpMap = new ConcurrentDictionary<string, string>();

        // Đồng bộ hóa cho việc lấy IP proxy
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> ProxyLocks = new ConcurrentDictionary<string, SemaphoreSlim>();

        // Lưu trạng thái tất cả các task đã gọi GetProxy
        private static readonly ConcurrentDictionary<string, HashSet<int>> WaitingTasksMap = new ConcurrentDictionary<string, HashSet<int>>();

        // Lưu trạng thái task đã lấy IP thành công hay chưa
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<int, bool>> TaskIpStatus = new ConcurrentDictionary<string, ConcurrentDictionary<int, bool>>();

        // Lưu thời gian task đã lấy IP thành công
        private static readonly ConcurrentDictionary<string, Stopwatch> TaskIpTime = new ConcurrentDictionary<string, Stopwatch>();

        public static double TimeGetProxy = 0;
        // Hàm lưu key proxy
        public static void AddProxy(string keyProxy)
        {
            ProxyTaskMap.TryAdd(keyProxy, new HashSet<int>());
            ProxyLocks.TryAdd(keyProxy, new SemaphoreSlim(1, 1));
            WaitingTasksMap.TryAdd(keyProxy, new HashSet<int>());
            TaskIpStatus.TryAdd(keyProxy, new ConcurrentDictionary<int, bool>());
            TaskIpTime.TryAdd(keyProxy, new Stopwatch());
        }

        // Hàm xóa key proxy
        public static void RemoveProxy(string keyProxy)
        {
            ProxyTaskMap.TryRemove(keyProxy, out _);
            ProxyIpMap.TryRemove(keyProxy, out _);
            WaitingTasksMap.TryRemove(keyProxy, out _);
            TaskIpStatus.TryRemove(keyProxy, out _);
            TaskIpTime.TryRemove(keyProxy, out _);
            if (ProxyLocks.TryRemove(keyProxy, out var semaphore))
            {
                semaphore.Dispose();
            }
        }

        // Hàm gắn id với key proxy
        public static void AssignTaskToProxy(string keyProxy, int taskId)
        {
            if (ProxyTaskMap.TryGetValue(keyProxy, out var taskIds))
            {
                lock (taskIds)
                {
                    taskIds.Add(taskId);
                }
                if (TaskIpStatus.TryGetValue(keyProxy, out var taskStatus))
                {
                    taskStatus.TryAdd(taskId, false);
                }
            }
        }

        // Hàm lấy IP proxy
        public static async Task<string> GetProxy(string keyProxy, int taskId)
        {
            if (!ProxyTaskMap.ContainsKey(keyProxy))
            {
                throw new ArgumentException($"Proxy key '{keyProxy}' không tồn tại.");
            }
            if (ProxyTaskMap.TryGetValue(keyProxy, out var taskIds))
            {
                lock (taskIds)
                {
                    if (!taskIds.Contains(taskId))
                    {
                        throw new ArgumentException($"TaskId in key '{keyProxy}' không tồn tại.");
                    }
                }
            }
            await ProxyLocks[keyProxy].WaitAsync();
            try
            {
                // Kiểm tra trạng thái task
                if (TaskIpStatus.TryGetValue(keyProxy, out var taskStatus1) && taskStatus1.TryGetValue(taskId, out var alreadyFetched1) && alreadyFetched1)
                {

                }
                else if (ProxyIpMap.TryGetValue(keyProxy, out var ipStv))
                {
                    // Đánh dấu taskId đã hoàn thành với IP hiện tại
                    if (TaskIpStatus.TryGetValue(keyProxy, out var statusMap))
                    {
                        statusMap[taskId] = true;
                    }
                    return ipStv;
                }
                // Thêm taskId vào danh sách đợi
                if (WaitingTasksMap.TryGetValue(keyProxy, out var waitingTasks))
                {
                    lock (waitingTasks)
                    {
                        waitingTasks.Add(taskId);
                    }
                }
                //Thread.Sleep(5000);
                // Kiểm tra nếu tất cả task đã gọi GetProxy hoặc gọi lần đầu.
                if (AllTasksWaiting(keyProxy) || !ProxyIpMap.TryGetValue(keyProxy, out var ipSt))
                {
                    if (TimeGetProxy != 0)//Giới hạn thời gian có thể lấy ip (giây).
                    {
                        if (TaskIpTime.TryGetValue(keyProxy, out var taskTime))
                        {
                            if (taskTime.IsRunning)
                            {
                                if (taskTime.Elapsed.TotalSeconds < TimeGetProxy)
                                {
                                    double ttimetp = TimeGetProxy - taskTime.Elapsed.TotalSeconds;
                                    int mnn = Convert.ToInt32(ttimetp);
                                    if (mnn < 1)
                                    {
                                        return string.Empty;
                                    }
                                    return mnn.ToString();
                                }
                            }
                            //else
                            //{
                            //    taskTime.Start();
                            //}
                        }
                    }
                    // Nếu chưa có IP proxy hoặc cần làm mới, gọi hàm LayProxy
                    var newIp = LayProxy(keyProxy);
                    if (newIp.Contains(":") && newIp.Contains("."))
                    {
                        ProxyIpMap[keyProxy] = newIp; // Lưu IP proxy mới
                                                      // Reset trạng thái task sau khi lấy IP mới
                        foreach (var task in TaskIpStatus[keyProxy].Keys)
                        {
                            TaskIpStatus[keyProxy][task] = false;
                        }
                        if (TimeGetProxy != 0)//Đặt lại thời gian có thể lấy ip (giây).
                        {
                            if (TaskIpTime.TryGetValue(keyProxy, out var taskTime))
                            {
                                taskTime.Restart();
                            }
                        }
                        // Xóa danh sách đợi
                        if (WaitingTasksMap.TryGetValue(keyProxy, out var tasks))
                        {
                            lock (tasks)
                            {
                                tasks.Clear();
                            }
                        }
                    }
                    else
                    {
                        return newIp;
                    }
                }
                // Kiểm tra trạng thái task
                if (TaskIpStatus.TryGetValue(keyProxy, out var taskStatus) && taskStatus.TryGetValue(taskId, out var alreadyFetched) && alreadyFetched)
                {
                    return string.Empty; // Trả về chuỗi rỗng nếu task đã lấy IP thành công trước đó
                }
                else if (ProxyIpMap.TryGetValue(keyProxy, out var ipSts))
                {
                    // Đánh dấu taskId đã hoàn thành với IP hiện tại
                    if (TaskIpStatus.TryGetValue(keyProxy, out var statusMap))
                    {
                        statusMap[taskId] = true;
                    }
                    return ipSts;
                }
                else
                {
                    return string.Empty; // Trả về chuỗi rỗng nếu chưa có IP.
                }
            }
            finally
            {
                ProxyLocks[keyProxy].Release();
            }
        }

        // Hàm lấy IP proxy chỉ với taskId
        public static async Task<string> GetProxy(int taskId)
        {
            var keyProxy = ProxyTaskMap.FirstOrDefault(kvp => kvp.Value.Contains(taskId)).Key;

            if (keyProxy == null)
            {
                throw new ArgumentException($"Task ID '{taskId}' không được gắn với key proxy nào.");
            }

            return await GetProxy(keyProxy, taskId);
        }
        public static string GetKeyByID(int taskId)
        {
            var keyProxy = ProxyTaskMap.FirstOrDefault(kvp => kvp.Value.Contains(taskId)).Key;

            if (keyProxy == null)
            {
                return "";
            }

            return keyProxy;
        }
        public delegate string LayProxyHandler(string keyProxy);
        public static event LayProxyHandler VoidLayProxy;

        // Hàm lấy IP proxy từ key proxy => thay thế hàm thuê/lấy proxy vào.
        private static string LayProxy(string keyProxy)
        {
            var VoidLayPx = VoidLayProxy;
            if (VoidLayPx == null)
            {
                throw new ArgumentException("Set event get proxy 'VoidLayProxy'");
            }
            else
            {
                return VoidLayPx.Invoke(keyProxy);
            }
        }

        // Kiểm tra xem tất cả task đã gọi GetProxy hay chưa
        private static bool AllTasksWaiting(string keyProxy)
        {
            if (!ProxyTaskMap.TryGetValue(keyProxy, out var allTasks) ||
                !WaitingTasksMap.TryGetValue(keyProxy, out var waitingTasks))
            {
                return false;
            }

            lock (allTasks)
                lock (waitingTasks)
                {
                    return allTasks.SetEquals(waitingTasks);
                }
        }

        // Hàm xóa id khỏi danh sách
        public static void RemoveTaskFromProxy(string keyProxy, int taskId)
        {
            if (ProxyTaskMap.TryGetValue(keyProxy, out var taskIds))
            {
                lock (taskIds)
                {
                    taskIds.Remove(taskId);
                }
                if (TaskIpStatus.TryGetValue(keyProxy, out var taskStatus))
                {
                    taskStatus.TryRemove(taskId, out _);
                }
            }

            if (WaitingTasksMap.TryGetValue(keyProxy, out var waitingTasks))
            {
                lock (waitingTasks)
                {
                    waitingTasks.Remove(taskId);
                }
            }
        }
    }
}
