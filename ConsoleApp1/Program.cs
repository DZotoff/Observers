using System;
using System.Collections.Generic;

// define the Observer interface
public interface Observer
{
    void Update(float temperature, float humidity, float pressure);
}
// define the Subject interface
public interface Subject
{
    void RegisterObserver(Observer observer);
    void RemoveObserver(Observer observer);
    void NotifyObservers();
}

// WeatherData class which implements the Subject interface
public class WeatherData : Subject
{
    private List<Observer> observers;
    private float temperature;
    private float humidity;
    private float pressure;

// constructor initializes the list of observers
    public WeatherData()
    {
        observers = new List<Observer>();
    }

    public void RegisterObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }

// notify registered observers when the state changes
    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(temperature, humidity, pressure);
        }
    }

    public void SetMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        measurementsChanged();
    }

 // method to trigger the notification to observers
    private void measurementsChanged()
    {
        NotifyObservers();
    }
}

// CurrentConditionsDisplay class which implements the Observer interface
public class CurrentConditionsDisplay : Observer
{
    public void Update(float temperature, float humidity, float pressure)
    {
        Console.WriteLine($"Current conditions: {temperature} degrees and {humidity}% humidity");
    }
}

// StatisticsDisplay class which implements the Observer interface
public class StatisticsDisplay : Observer
{
    private List<float> tempHistory;

    public StatisticsDisplay()
    {
        tempHistory = new List<float>();
    }

    public void Update(float temperature, float humidity, float pressure)
    {
        tempHistory.Add(temperature);

        float average = tempHistory.Average();
        float max = tempHistory.Max();
        float min = tempHistory.Min();

        Console.WriteLine($"Avg/Max/Min temperature: {average}/{max}/{min}");
    }
}
// ForecastDisplay class which implements the Observer interface
public class ForecastDisplay : Observer
{
    public void Update(float temperature, float humidity, float pressure)
    {
        string forecastMessage = GetForecastMessage(temperature);
        Console.WriteLine($"Forecast: {forecastMessage}");
    }
    private string GetForecastMessage(float temperature)
    {
        if (temperature > 80)
        {
            return "Improving weather on the way!";
        }
        else if (temperature < 50)
        {
            return "Watch out for cooler, rainy weather";
        }
        else
        {
            return "More of the same";
        }
    }
}
// user's code
class Program
{
    static void Main()
    {

 // WeatherData and observer displays
        WeatherData weatherData = new WeatherData();

        CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay();
        StatisticsDisplay statisticsDisplay = new StatisticsDisplay();
        ForecastDisplay forecastDisplay = new ForecastDisplay();


 // register observers with the subject
        weatherData.RegisterObserver(currentDisplay);
        weatherData.RegisterObserver(statisticsDisplay);
        weatherData.RegisterObserver(forecastDisplay);

 // set and display measurements
        weatherData.SetMeasurements(80, 65, 0);
        Console.WriteLine();

        weatherData.SetMeasurements(82, 70, 0);
        Console.WriteLine();

        weatherData.SetMeasurements(78, 90, 0);
    }
}