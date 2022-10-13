using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
	/// <summary>
	/// A simple, Unity-friendly timer that is operated by hand.
	/// </summary>
	[Serializable]
	public class ManualTimer : IEquatable<ManualTimer>
	{
		[SerializeField] private float duration;
		[SerializeField] private float elapsed;

		/// <summary>
		/// Create a new timer that will run for the given duration.
		/// 
		/// <para>
		/// The timer initially starts at full, and will begin counting down as
		/// Update() is called.
		/// </para>
		/// 
		/// </summary>
		/// <param name="duration">How long the timer lasts.</param>
		/// <exception cref="ArgumentOutOfRangeException">If duration is not positive.</exception>
		public ManualTimer(float duration)
		{
			Duration = duration;
			elapsed = 0;
		}

		/// <summary>
		/// Create a timer with a default duration of 1 second.
		/// 
		/// <para>
		/// This constructor exists solely for compatibility with Unity.
		/// </para>
		/// 
		/// </summary>
		public ManualTimer() : this(1)
		{
		}

		/// <summary>
		/// Create an exact replica of another timer. Both the timer duration
		/// and amount of time remaining in the timer is copied.
		/// </summary>
		/// <param name="other">Timer to make a copy of.</param>
		public ManualTimer(ManualTimer other)
		{
			duration = other.duration;
			elapsed = other.elapsed;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as ManualTimer);
		}

		/// <summary>
		/// Timers with the same max duration are equal.
		/// </summary>
		public bool Equals(ManualTimer other)
		{
			return other != null &&
				   elapsed == other.elapsed &&
				   duration == other.duration;
		}

		public override int GetHashCode()
		{
			return 2065618965 + duration.GetHashCode() ^ elapsed.GetHashCode();
		}

		public static bool operator ==(ManualTimer left, ManualTimer right)
		{
			return EqualityComparer<ManualTimer>.Default.Equals(left, right);
		}

		public static bool operator !=(ManualTimer left, ManualTimer right)
		{
			return !(left == right);
		}

		/// <summary>
		/// Amount of time that has passed since the timer has started.
		/// 
		/// <para>
		/// This value starts at 0 and counts up to the timer's duration. The
		/// value is guaranteed to be in [0, duration].
		/// </para>
		/// 
		/// </summary>
		public float ElapsedTime
		{
			get
			{
				return Mathf.Clamp(elapsed, 0, duration);
			}
		}

		/// <summary>
		/// Amount of time remaining before the timer finishes.
		/// 
		/// <para>
		/// This value starts at the timer's duration and counts down to 0. The
		/// value is guaranteed to be in [0, duration].
		/// </para>
		/// 
		/// </summary>
		public float RemainingTime
		{
			get
			{
				return Mathf.Clamp(duration - elapsed, 0, duration);
			}
		}

		/// <summary>
		/// How long the timer will run for before finishing. This number must be positive.
		/// 
		/// <para>
		/// Changing the duration while the timer is running will preserve the
		/// amount of elapsed time. This elapsed time will count toward the new
		/// duration. However, no more than the original duration will be preserved.
		/// </para>
		/// 
		/// <para>
		/// Example: A timer has a duration of 4 seconds and has been running
		/// for 8 seconds. The user changes the duration to be 10 seconds. The
		/// timer is now 4 seconds in to the next countdown of 10 seconds.
		/// </para>
		/// 
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">If value is not positive.</exception>
		public float Duration
		{
			get => duration;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("SimpleTimer: Timer duration must be positive.");
				}
				if (elapsed > duration)
				{
					elapsed = duration;
				}
				duration = value;
			}
		}

		/// <summary>
		/// Returns true if the timer has completed.
		/// 
		/// <para>
		/// This value will remain true until Start() has been called again.
		/// </para>
		/// 
		/// <para>
		/// <see cref="Done"/> and <see cref="Running"/> are completely
		/// disjoint. Only one can be true at a time.
		/// </para>
		/// 
		/// </summary>
		public bool Done
		{
			get
			{
				return elapsed >= duration;
			}
		}

		/// <summary>
		/// Returns true if the timer is still in progress.
		/// 
		/// <para>
		/// <see cref="Done"/> and <see cref="Running"/> are completely
		/// disjoint. Only one can be true at a time.
		/// </para>
		/// 
		/// </summary>
		public bool Running
		{
			get
			{
				return elapsed < duration;
			}
		}

		/// <summary>
		/// Returns the timer's progress toward completion as a value between [0, 1].
		/// 
		/// <para>
		/// When the timer starts, this value is exactly 0. As the timer is
		/// updated, this value moves toward 1 at a linear rate. Once the timer
		/// is finished, the normalized progress will be exactly 1. At any
		/// time, the normalized progress is guaranteed to be within [0, 1].
		/// </para>
		/// 
		/// </summary>
		public float NormalizedProgress
		{
			get
			{
				return Mathf.Clamp01(elapsed / duration);
			}
		}

		/// <summary>
		/// Refill the time and prepare to start counting down.
		/// </summary>
		public void Start()
		{
			elapsed = 0;
		}

		/// <summary>
		/// Immediately end the countdown.
		/// 
		/// <para>
		/// Calling this function will cause Done to immediately return true.
		/// </para>
		/// 
		/// </summary>
		public void Stop()
		{
			elapsed = duration;
		}

		/// <summary>
		/// Update the amount of timer remaining in the timer.
		/// 
		/// <para>
		/// This function <strong>must</strong> be called every update period for the
		/// timer to function. If your timer stays stuck at a value forever,
		/// check to make sure that you calling Update().
		/// </para>
		/// 
		/// </summary>
		/// <param name="delta">The amount of time that has passed between the last call to Update().</param>
		public void Update(float delta)
		{
			if (elapsed < duration)
			{
				elapsed += delta;
			}
		}

		public override string ToString()
		{
			return $"SimpleTimer({ElapsedTime}s / {Duration}s)";
		}
	}
}
