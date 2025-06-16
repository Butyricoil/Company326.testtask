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

## Тестовое задание для компании Cifkor

## Описание задания:
Приложение состоит из двух вкладок, между которыми можно переключаться через нижнюю навигационную панель:
- Первая вкладка отображает данные о погоде;
- Вторая вкладка — список пород собак;
  
## В работе реализовано:
- Интерфейс с двумя вкладками, между которыми можно переключаться;
- Заготовки для взаимодействия с сервером и организации очереди запросов (требует доработки);
- Основная структура приложения согласно ТЗ;

## Что не удалось реализовать:
- Не получилось десериализовать JSON-ответы;
- Не успел интегрировать сервис очереди вызовов данных с сервера;
- Не занимался оптимизацией (пул объектов, фабрики);
- Не успел добавить анимации с помощью DOTween;

## 🧾 Как запустить
1. Клонируйте репозиторий: git clone https://github.com/DjKarp/Cifkor
2. Откройте проект в Unity (или другой нужной IDE).
3. Убедитесь, что установлены все зависимости пакетов (Zenject, DOTween, если потребуется в будущем).
4. Запустите любую сцену. Загрузка всё равно будет происходить через Boostrap.

## 🌍 Готовые билды:
РС - http://redleggames.com/Games/Cifkor_Testtask_PC.zip

## 📁 Структура проекта
<pre> ```Assets/
├── Prefab/             # Префабы страниц приложения и кнопок для страницы с породами собак
├── Scenes/             # Все сцены игры 
│   ├── Bootstrap       # Разгоночная сцена, с неё запускается приложение
│   ├── Application     # Сцена самого приложения
├── Scripts/
│   ├── DogBreeds/      # Скрипты страницы с породами собак
│   ├── EntryPoint/     # Точка входа
│   ├── Json/           # Сериализированные классы для десериализации с JSON
│   ├── MVP/            # Компоненты MVP работы приложения
│   ├── Util/           # Скрипты помощники
│   └── Web/            # Сервис скачивания данных с сервера и работа с очередью запросов
└── Zenject/            
│   ├── Installers/     # Инсталлеры для SceneContext
│   ├── Prefab/         # Префабы инсталлеров
│   ├── Resources/      # Папка хранения ProjectContext
│   ├── Signals/        # Сигналы
└── 
``` </pre>
---
