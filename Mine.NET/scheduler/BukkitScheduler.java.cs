using Mine.NET.plugin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mine.NET.scheduler
{
    public delegate void SchedulerAction();
    public interface BukkitScheduler
    {
        /**
         * Calls a method on the main thread and returns a Future object. This
         * task will be executed by the main server thread.
         * <ul>
         * <li>Note: The Future.get() methods must NOT be called from the main
         *     thread.
         * <li>Note2: There is at least an average of 10ms latency until the
         *     isDone() method returns true.
         * </ul>
         * @param <T> The callable's return type
         * @param plugin Plugin that owns the task
         * @param task Task to be executed
         * @return Future Future object related to the task
         */ //Use Linq for params
        Task<T> callSyncMethod<T>(Plugin plugin, SchedulerAction task);

        /**
         * Removes all tasks associated with a particular plugin from the
         * scheduler.
         *
         * @param plugin Owner of tasks to be removed
         */
        void cancelTasks(Plugin plugin);

        /**
         * Removes all tasks from the scheduler.
         */
        void cancelAllTasks();

        /**
         * Returns a list of all active workers.
         * <p>
         * This list contains asynch tasks that are being executed by separate
         * threads.
         *
         * @return Active workers
         */
        List<BukkitWorker> getActiveWorkers();

        /**
         * Returns a list of all pending tasks. The ordering of the tasks is not
         * related to their order of execution.
         *
         * @return Active workers
         */
        List<BukkitTask> getPendingTasks();

        /**
         * Returns a task that will run on the next server tick.
         *
         * @param plugin the reference to the plugin scheduling task
         * @param task the task to be run
         * @return a BukkitTask that contains the id number
         * @throws ArgumentException if plugin is null
         * @throws ArgumentException if task is null
         */
        BukkitTask runTask(Plugin plugin, SchedulerAction task);

        /**
         * <b>Asynchronous tasks should never access any API in Bukkit. Great care
         * should be taken to assure the thread-safety of asynchronous tasks.</b>
         * <p>
         * Returns a task that will run asynchronously.
         *
         * @param plugin the reference to the plugin scheduling task
         * @param task the task to be run
         * @return a BukkitTask that contains the id number
         * @throws ArgumentException if plugin is null
         * @throws ArgumentException if task is null
         */
        BukkitTask runTaskAsynchronously(Plugin plugin, SchedulerAction task);

        /**
         * Returns a task that will run after the specified number of server
         * ticks.
         *
         * @param plugin the reference to the plugin scheduling task
         * @param task the task to be run
         * @param delay the ticks to wait before running the task
         * @return a BukkitTask that contains the id number
         * @throws ArgumentException if plugin is null
         * @throws ArgumentException if task is null
         */
        BukkitTask runTaskLater(Plugin plugin, SchedulerAction task, long delay);

        /**
         * <b>Asynchronous tasks should never access any API in Bukkit. Great care
         * should be taken to assure the thread-safety of asynchronous tasks.</b>
         * <p>
         * Returns a task that will run asynchronously after the specified number
         * of server ticks.
         *
         * @param plugin the reference to the plugin scheduling task
         * @param task the task to be run
         * @param delay the ticks to wait before running the task
         * @return a BukkitTask that contains the id number
         * @throws ArgumentException if plugin is null
         * @throws ArgumentException if task is null
         */
        BukkitTask runTaskLaterAsynchronously(Plugin plugin, SchedulerAction task, long delay);

        /**
         * Returns a task that will repeatedly run until cancelled, starting after
         * the specified number of server ticks.
         *
         * @param plugin the reference to the plugin scheduling task
         * @param task the task to be run
         * @param delay the ticks to wait before running the task
         * @param period the ticks to wait between runs
         * @return a BukkitTask that contains the id number
         * @throws ArgumentException if plugin is null
         * @throws ArgumentException if task is null
         */
        BukkitTask runTaskTimer(Plugin plugin, SchedulerAction task, long delay, long period);

        /**
         * <b>Asynchronous tasks should never access any API in Bukkit. Great care
         * should be taken to assure the thread-safety of asynchronous tasks.</b>
         * <p>
         * Returns a task that will repeatedly run asynchronously until cancelled,
         * starting after the specified number of server ticks.
         *
         * @param plugin the reference to the plugin scheduling task
         * @param task the task to be run
         * @param delay the ticks to wait before running the task for the first
         *     time
         * @param period the ticks to wait between runs
         * @return a BukkitTask that contains the id number
         * @throws ArgumentException if plugin is null
         * @throws ArgumentException if task is null
         */
        BukkitTask runTaskTimerAsynchronously(Plugin plugin, SchedulerAction task, long delay, long period);
    }
}
