namespace Mine.NET.scheduler{

/**
 * This class is provided as an easy way to handle scheduling tasks.
 */
public abstract class BukkitRunnable : Runnable {
    private int taskId = -1;

    /**
     * Attempts to cancel this task.
     *
     * @throws InvalidOperationException if task was not scheduled yet
     */
    public void cancel() {
        Bukkit.getScheduler().cancelTask(getTaskId());
    }

    /**
     * Schedules this in the Bukkit scheduler to run on next tick.
     *
     * @param plugin the reference to the plugin scheduling task
     * @return a BukkitTask that contains the id number
     * @throws ArgumentException if plugin is null
     * @throws InvalidOperationException if this was already scheduled
     * @see BukkitScheduler#runTask(Plugin, Runnable)
     */
    public BukkitTask runTask(Plugin plugin) {
        checkState();
        return setupId(Bukkit.getScheduler().runTask(plugin, (Runnable) this));
    }

    /**
     * <b>Asynchronous tasks should never access any API in Bukkit. Great care
     * should be taken to assure the thread-safety of asynchronous tasks.</b>
     * <p>
     * Schedules this in the Bukkit scheduler to run asynchronously.
     *
     * @param plugin the reference to the plugin scheduling task
     * @return a BukkitTask that contains the id number
     * @throws ArgumentException if plugin is null
     * @throws InvalidOperationException if this was already scheduled
     * @see BukkitScheduler#runTaskAsynchronously(Plugin, Runnable)
     */
    public BukkitTask runTaskAsynchronously(Plugin plugin)  {
        checkState();
        return setupId(Bukkit.getScheduler().runTaskAsynchronously(plugin, (Runnable) this));
    }

    /**
     * Schedules this to run after the specified number of server ticks.
     *
     * @param plugin the reference to the plugin scheduling task
     * @param delay the ticks to wait before running the task
     * @return a BukkitTask that contains the id number
     * @throws ArgumentException if plugin is null
     * @throws InvalidOperationException if this was already scheduled
     * @see BukkitScheduler#runTaskLater(Plugin, Runnable, long)
     */
    public BukkitTask runTaskLater(Plugin plugin, long delay)  {
        checkState();
        return setupId(Bukkit.getScheduler().runTaskLater(plugin, (Runnable) this, delay));
    }

    /**
     * <b>Asynchronous tasks should never access any API in Bukkit. Great care
     * should be taken to assure the thread-safety of asynchronous tasks.</b>
     * <p>
     * Schedules this to run asynchronously after the specified number of
     * server ticks.
     *
     * @param plugin the reference to the plugin scheduling task
     * @param delay the ticks to wait before running the task
     * @return a BukkitTask that contains the id number
     * @throws ArgumentException if plugin is null
     * @throws InvalidOperationException if this was already scheduled
     * @see BukkitScheduler#runTaskLaterAsynchronously(Plugin, Runnable, long)
     */
    public BukkitTask runTaskLaterAsynchronously(Plugin plugin, long delay)  {
        checkState();
        return setupId(Bukkit.getScheduler().runTaskLaterAsynchronously(plugin, (Runnable) this, delay));
    }

    /**
     * Schedules this to repeatedly run until cancelled, starting after the
     * specified number of server ticks.
     *
     * @param plugin the reference to the plugin scheduling task
     * @param delay the ticks to wait before running the task
     * @param period the ticks to wait between runs
     * @return a BukkitTask that contains the id number
     * @throws ArgumentException if plugin is null
     * @throws InvalidOperationException if this was already scheduled
     * @see BukkitScheduler#runTaskTimer(Plugin, Runnable, long, long)
     */
    public BukkitTask runTaskTimer(Plugin plugin, long delay, long period)  {
        checkState();
        return setupId(Bukkit.getScheduler().runTaskTimer(plugin, (Runnable) this, delay, period));
    }

    /**
     * <b>Asynchronous tasks should never access any API in Bukkit. Great care
     * should be taken to assure the thread-safety of asynchronous tasks.</b>
     * <p>
     * Schedules this to repeatedly run asynchronously until cancelled,
     * starting after the specified number of server ticks.
     *
     * @param plugin the reference to the plugin scheduling task
     * @param delay the ticks to wait before running the task for the first
     *     time
     * @param period the ticks to wait between runs
     * @return a BukkitTask that contains the id number
     * @throws ArgumentException if plugin is null
     * @throws InvalidOperationException if this was already scheduled
     * @see BukkitScheduler#runTaskTimerAsynchronously(Plugin, Runnable, long,
     *     long)
     */
    public BukkitTask runTaskTimerAsynchronously(Plugin plugin, long delay, long period)  {
        checkState();
        return setupId(Bukkit.getScheduler().runTaskTimerAsynchronously(plugin, (Runnable) this, delay, period));
    }

    /**
     * Gets the task id for this runnable.
     *
     * @return the task id that this runnable was scheduled as
     * @throws InvalidOperationException if task was not scheduled yet
     */
    public int getTaskId() {
        readonly int id = taskId;
        if (id == -1) {
            throw new InvalidOperationException("Not scheduled yet");
        }
        return id;
    }

    private void checkState() {
        if (taskId != -1) {
            throw new InvalidOperationException("Already scheduled as " + taskId);
        }
    }

    private BukkitTask setupId(BukkitTask task) {
        this.taskId = task.getTaskId();
        return task;
    }
}
