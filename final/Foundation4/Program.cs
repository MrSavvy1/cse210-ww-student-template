// Activity.cs
public abstract class Activity
{
		private string date;
		private int lengthInMinutes;

		public Activity(string date, int lengthInMinutes)
		{
				this.date = date;
				this.lengthInMinutes = lengthInMinutes;
		}

		public string Date => date;
		public int LengthInMinutes => lengthInMinutes;

		public abstract float GetDistance();
		public abstract float GetSpeed();
		public abstract float GetPace();
		public abstract string GetSummary();
}

// Running.cs
public class Running : Activity
{
		private float distance;

		public Running(string date, int lengthInMinutes, float distance)
				: base(date, lengthInMinutes)
		{
				this.distance = distance;
		}

		public override float GetDistance()
		{
				return distance;
		}

		public override float GetSpeed()
		{
				return distance / (LengthInMinutes / 60.0f);
		}

		public override float GetPace()
		{
				return LengthInMinutes / distance;
		}

		public override string GetSummary()
		{
				return $"Running on {Date} for {LengthInMinutes} minutes: Distance = {distance} km, Speed = {GetSpeed()} km/h, Pace = {GetPace()} min/km";
		}
}

// Cycling.cs
public class Cycling : Activity
{
		private float speed;

		public Cycling(string date, int lengthInMinutes, float speed)
				: base(date, lengthInMinutes)
		{
				this.speed = speed;
		}

		public override float GetDistance()
		{
				return speed * (LengthInMinutes / 60.0f);
		}

		public override float GetSpeed()
		{
				return speed;
		}

		public override float GetPace()
		{
				return 60.0f / speed;
		}

		public override string GetSummary()
		{
				return $"Cycling on {Date} for {LengthInMinutes} minutes: Speed = {speed} km/h, Distance = {GetDistance()} km, Pace = {GetPace()} min/km";
		}
}

// Swimming.cs
public class Swimming : Activity
{
		private int laps;

		public Swimming(string date, int lengthInMinutes, int laps)
				: base(date, lengthInMinutes)
		{
				this.laps = laps;
		}

		public override float GetDistance()
		{
				return laps * 0.05f; // Assuming each lap is 50 meters
		}

		public override float GetSpeed()
		{
				return GetDistance() / (LengthInMinutes / 60.0f);
		}

		public override float GetPace()
		{
				return LengthInMinutes / GetDistance();
		}

		public override string GetSummary()
		{
				return $"Swimming on {Date} for {LengthInMinutes} minutes: Laps = {laps}, Distance = {GetDistance()} km, Speed = {GetSpeed()} km/h, Pace = {GetPace()} min/km";
		}
}

// Program.cs
class Program
{
		static void Main(string[] args)
		{
				Running running = new Running("2024-07-20", 30, 5);
				Cycling cycling = new Cycling("2024-07-21", 45, 20);
				Swimming swimming = new Swimming("2024-07-22", 60, 30);

				List<Activity> activities = new List<Activity> { running, cycling, swimming };

				foreach (var activity in activities)
				{
						Console.WriteLine(activity.GetSummary());
				}
		}
}
