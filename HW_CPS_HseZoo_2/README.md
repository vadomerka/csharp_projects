HseZoo2

Clean Architecture + DDD

# Архитектура
## Domain
### Aggregates
- Enclosure : Вольер, содержащий сущности, которые можно поместить в вольер
### Entities
- Animal : Животное
### Events
События зоопарка
- AnimalMovedEvent
- FeedingTimeEvent
### Factories
Фабрики соответсвующих сущностей
- AnimalFactory
- EnclosureFactory 
- FeedingScheduleFactory
### Repositories
Репозитории сущностей зоопарка
- EnclosureRepository 
- FeedingScheduleRepository 

Репозитории статистики
- FeedingStatisticsRepository 
- MovingStatisticsRepository
### ValueObject
Объекты для переноса данных
- AnimalDTO
- FeedingScheduleDTO 

Объекты - данные
- EnclosureSize 
- Gender
## UseCases
- AnimalDataService - создает и возвращает Животных
- AnimalTransferService - добавляет удалает и перемещает Животных
- EnclosureDataService - находит, создает, добавляет и удаляет Вольеры
- FeedingOrganizationService - находит, создает, добавляет и удаляет Расписание. Реализует логику выполнения кормежки.
## Interface
Управление объектами
- AnimalFacade
- EnclosureFacade 
- FeedingScheduleFacade 
- ZooStatisticsFacade
## Controllers
WebApi
- AnimalController
- EnclosureController
- FeedingTimeController
- StatisticsController

#
# Реализация требований
1. Реализован DDD - принципы разделения объектов на aggregate, entity и value-object
2. Реализована Clean Architecture - слои представлены выше. Связь между слоями происходит через предыдущий слой. Зависимостей сверху вниз нет.
3. Реализовано WebApi - контроллеры могут добавлять/удалять объекты, а также выполняют дополнительные функции - кормежки (через расписание), просмотр статистики
4. Реализация Swagger успешно тестируется при запуске приложения и переходе по ссылке https://localhost:7027/swagger/index.html (или же по другому доступному порту)
5. Все данные хранятся в памяти
6. Отчет вы сейчас читаете.
- Функционал в основном реализован через UseCases, я постарался вынести все лишнее из Домена. Все добавления/удаления и остальная работа с репозиториями реализована именно в этом слое. Также как и сбор статистики, функционал кормления по таймеру и перемещения животных.
- Interfaces это уже Facade для удобного использования контроллерами. Они объединяют отдельные функции UseCases, как например функции создания и добавления объекта к репозиторию, таким образом, чтобы добавление объекта было одной функцией.
- Зависимости реализованы через IServiceProvider, что очень упростило код, а также упростило тестирование.
- Отчет по адресу HseZoo2_Tests/TestResults/e97663d1-907f-4a0e-a0c0-7037fde3aed0/html/index.html содержит результаты прогона xUnit тестов (66%)