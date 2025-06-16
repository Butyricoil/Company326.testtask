# Weather & Dog Breeds Application

## 🎯 Overview
A Unity application that demonstrates advanced architectural patterns and efficient server communication. The application features two main tabs: Weather Information and Dog Breeds List, with a robust request queue system for handling API calls.

## 🛠️ Technical Stack
- **Unity Version**: 2022.3+
- **Core Technologies**:
  - Zenject for dependency injection
  - UnityWebRequest for HTTP communication
  - MVC/MVP architecture pattern
  - Custom request queue system
- **Optional Enhancements**:
  - UniRx + UniTask for reactive programming
  - DOTween for smooth animations

## 🏗️ Architecture Highlights

### Request Queue System
- Implemented a robust request queue system that ensures sequential API calls
- Features request cancellation and queue management
- Handles concurrent requests efficiently
- Supports request prioritization and cleanup

### Weather Tab
- Real-time weather updates every 5 seconds
- Automatic request cancellation when tab is inactive
- Clean request queue management
- Weather display format: (weather icon) Today - 61F

### Dog Breeds Tab
- Efficient breed list loading with loading indicators
- Interactive breed selection
- Dynamic popup system for breed details
- Adaptive content height based on description length
- Smart request management for breed details

## 🎨 UI/UX Features
- Smooth tab switching with bottom navigation
- Loading indicators for async operations
- Responsive popup system
- Clean and intuitive interface

## 📦 Project Structure
```
Assets/
├── Prefabs/            # UI prefabs and reusable components
├── Scenes/            
│   ├── Bootstrap      # Application entry point
│   └── Main          # Main application scene
├── Scripts/
│   ├── Core/         # Core systems and interfaces
│   ├── Models/       # Data models
│   ├── Views/        # UI components
│   ├── Presenters/   # Business logic
│   ├── Services/     # API and utility services
│   └── Utils/        # Helper classes
└── Zenject/          
    ├── Installers/   # Dependency injection setup
    └── Signals/      # Event system
```

## 🚀 Key Features

### Request Management
- Sequential request execution
- Request cancellation support
- Queue cleanup on tab switch
- Error handling and retry mechanisms

### Weather Implementation
- Periodic weather updates
- Efficient request cancellation
- Clean data presentation
- Error state handling

### Dog Breeds Implementation
- Efficient breed list loading
- Interactive breed selection
- Dynamic popup system
- Smart request management
- Loading state indicators

## 🎮 How to Run
1. Clone the repository
2. Open the project in Unity 2022.3 or later
3. Open the Bootstrap scene
4. Press Play

## 📱 Builds
- Windows: [Download Link]
- Android: [Download Link]
- iOS: [Download Link]

## 🎯 Performance Optimizations
- Object pooling for UI elements
- Efficient request queue management
- Smart resource loading
- Memory leak prevention

## 🔄 Future Improvements
- Additional weather details
- Breed image gallery
- Offline mode support
- Enhanced animations
- Unit test coverage

## 📝 Notes
- All API endpoints are properly configured
- Error handling is implemented throughout
- Code follows SOLID principles
- Documentation is available in code comments

## 💻 Code Usage Examples

### Web Request Service

```csharp
// Initialize the service
private WebRequestService _webRequestService = new WebRequestService();

// Example 1: Get weather data
public void GetWeatherData()
{
    _webRequestService.RequestJSON(
        "https://api.weather.gov/gridpoints/TOP/32,81/forecast",
        progress => Debug.Log($"Download progress: {progress}"),
        response => {
            if (response != null)
            {
                // Process weather data
                Debug.Log($"Weather data received: {response}");
            }
        },
        () => Debug.Log("Weather request completed"),
        true, // High priority
        RequestType.Weather
    );
}

// Example 2: Get dog breeds list
public void GetDogBreeds()
{
    _webRequestService.RequestJSON(
        "https://api.thedogapi.com/v1/breeds",
        progress => Debug.Log($"Download progress: {progress}"),
        response => {
            if (response != null)
            {
                // Process breeds data
                Debug.Log($"Breeds data received: {response}");
            }
        },
        () => Debug.Log("Breeds request completed"),
        false, // Normal priority
        RequestType.DogBreeds
    );
}

// Example 3: Get specific breed details
public void GetBreedDetails(string breedId)
{
    _webRequestService.RequestJSON(
        $"https://api.thedogapi.com/v1/breeds/{breedId}",
        progress => Debug.Log($"Download progress: {progress}"),
        response => {
            if (response != null)
            {
                // Process breed details
                Debug.Log($"Breed details received: {response}");
            }
        },
        () => Debug.Log("Breed details request completed"),
        true, // High priority
        RequestType.DogBreedDetails
    );
}

// Example 4: Cancel specific type of requests
public void CancelWeatherRequests()
{
    _webRequestService.CancelRequestsByType(RequestType.Weather);
}

// Example 5: Cancel all requests
public void CancelAllRequests()
{
    _webRequestService.CancelAllRequests();
}
```

### Task Service

```csharp
// Initialize the service
private TaskService _taskService = new TaskService();

// Example 1: Add a high priority task
public void AddHighPriorityTask()
{
    _taskService.AddTask(
        YourCoroutine(),
        () => Debug.Log("Task completed"),
        true, // High priority
        RequestType.Weather
    );
}

// Example 2: Add a normal priority task
public void AddNormalPriorityTask()
{
    _taskService.AddTask(
        YourCoroutine(),
        () => Debug.Log("Task completed"),
        false, // Normal priority
        RequestType.DogBreeds
    );
}

// Example 3: Cancel tasks by type
public void CancelTasksByType()
{
    _taskService.CancelTasksByType(RequestType.Weather);
}

// Example 4: Stop current task
public void StopCurrentTask()
{
    _taskService.StopCurrentTask();
}

// Example 5: Clear all tasks
public void ClearAllTasks()
{
    _taskService.Clear();
}
```

### Task Implementation

```csharp
// Example 1: Create and start a task
public void CreateAndStartTask()
{
    var task = Task.Create(YourCoroutine())
        .Subscribe(() => Debug.Log("Task completed"));
    
    task.Start();
}

// Example 2: Create a task with type
public void CreateTypedTask()
{
    var task = Task.Create(YourCoroutine());
    task.Type = RequestType.Weather;
    task.Subscribe(() => Debug.Log("Task completed"))
        .Start();
}

// Example 3: Stop a task
public void StopTask(Task task)
{
    task.Stop();
}
```

## 🔄 Request Queue Behavior

The request queue system ensures that:
1. Requests are executed sequentially
2. High priority requests are processed first
3. Requests can be cancelled by type
4. Failed requests are retried automatically
5. Resources are properly cleaned up

Example queue behavior:
```csharp
// These requests will be executed in order:
// 1. High priority weather request
// 2. High priority breed details request
// 3. Normal priority breeds list request

_webRequestService.RequestJSON(weatherUrl, ..., true, RequestType.Weather);
_webRequestService.RequestJSON(breedDetailsUrl, ..., true, RequestType.DogBreedDetails);
_webRequestService.RequestJSON(breedsListUrl, ..., false, RequestType.DogBreeds);
```

## ⚠️ Important Notes

1. Always cancel requests when switching tabs or leaving scenes
2. Use appropriate request types for proper queue management
3. Handle null responses in callbacks
4. Monitor request progress for large downloads
5. Clean up resources by calling CancelAllRequests when needed

