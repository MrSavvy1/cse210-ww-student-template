using System;
using System.Collections.Generic;
using System.Text;

public class Address
{
		private string street;
		private string city;
		private string state;
		private string country;

		public Address(string street, string city, string state, string country)
		{
				this.street = street;
				this.city = city;
				this.state = state;
				this.country = country;
		}

		public bool IsInUSA()
		{
				return country.ToLower() == "usa";
		}

		public override string ToString()
		{
				return $"{street}, {city}, {state}, {country}";
		}
}

public class Event
{
		private string title;
		private string description;
		private string date;
		private string time;
		private Address address;

		public Event(string title, string description, string date, string time, Address address)
		{
				this.title = title;
				this.description = description;
				this.date = date;
				this.time = time;
				this.address = address;
		}

		public virtual string GetStandardDetails()
		{
				return $"Title: {title}\nDescription: {description}\nDate: {date}\nTime: {time}\nAddress: {address}";
		}

		public virtual string GetFullDetails()
		{
				return GetStandardDetails();
		}

		public virtual string GetShortDescription()
		{
				return $"Title: {title}, Date: {date}, Time: {time}";
		}
}

public class Lecture : Event
{
		private string speakerName;
		private int capacity;

		public Lecture(string title, string description, string date, string time, Address address, string speakerName, int capacity)
				: base(title, description, date, time, address)
		{
				this.speakerName = speakerName;
				this.capacity = capacity;
		}

		public override string GetFullDetails()
		{
				return $"{base.GetFullDetails()}\nSpeaker: {speakerName}\nCapacity: {capacity}";
		}
}

public class Reception : Event
{
		private string rsvpEmail;

		public Reception(string title, string description, string date, string time, Address address, string rsvpEmail)
				: base(title, description, date, time, address)
		{
				this.rsvpEmail = rsvpEmail;
		}

		public override string GetFullDetails()
		{
				return $"{base.GetFullDetails()}\nRSVP Email: {rsvpEmail}";
		}
}

public class OutdoorGathering : Event
{
		private string weatherForecast;

		public OutdoorGathering(string title, string description, string date, string time, Address address, string weatherForecast)
				: base(title, description, date, time, address)
		{
				this.weatherForecast = weatherForecast;
		}

		public override string GetFullDetails()
		{
				return $"{base.GetFullDetails()}\nWeather Forecast: {weatherForecast}";
		}
}

class Program
{
		static void Main(string[] args)
		{
				Address address = new Address("456 Park Ave", "New York", "NY", "USA");

				Lecture lecture = new Lecture("Tech Talk", "Learn about the latest in tech", "2024-08-01", "10:00 AM", address, "Dr. Tech", 100);
				Reception reception = new Reception("Company Party", "Annual company gathering", "2024-08-02", "6:00 PM", address, "rsvp@company.com");
				OutdoorGathering gathering = new OutdoorGathering("Picnic", "Company picnic", "2024-08-03", "12:00 PM", address, "Sunny");

				List<Event> events = new List<Event> { lecture, reception, gathering };

				foreach (var ev in events)
				{
						Console.WriteLine(ev.GetFullDetails());
						Console.WriteLine();
				}
		}
}
