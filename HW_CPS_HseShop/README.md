# HseShop

## Структура проекта

Проект представляет собой микросервисную архитектуру, состоящую из двух основных сервисов: OrdersService и PaymentsService, которые взаимодействуют через паттерн Outbox/Inbox с использованием Kafka.

## OrdersService

**OrdersService** отвечает за управление заказами в системе.

### Состав:

* **Web**: API контроллеры и конфигурация приложения
* **Entities**: Доменные модели (Order, OrderState)
* **UseCases**: Бизнес-логика (OrderFactory)
* **Infrastructure**: 
  * Репозитории (OrderRepository)
  * Фасады (OrderFacade)
  * Контекст базы данных (OrderDBContext)
  * Реализация Outbox/Inbox паттерна

### Функции:

* Создание и управление заказами
* Отслеживание статуса заказов
* Отправка уведомлений о статусе заказа в PaymentsService

## PaymentsService

**PaymentsService** отвечает за управление платежами и балансом пользователей.

### Состав:

* **Web**: API контроллеры и конфигурация приложения
* **Entities**: Доменные модели (Account)
* **UseCases**: Бизнес-логика
* **Infrastructure**:
  * Репозитории (AccountRepository)
  * Фасады (OrderExecuter, FindAccountFacade)
  * Контекст базы данных (AccountDBContext)
  * Реализация Outbox/Inbox паттерна

### Функции:

* Управление балансом пользователей
* Обработка платежей
* Проверка возможности оплаты заказа

## Реализация Outbox/Inbox паттерна

### Outbox Pattern

1. **Структура Outbox**:
   * Таблица `OrderStatus` в базе данных для хранения исходящих сообщений
   * Поля: Id, Payload (JSON), IsSent, CreatedAt

2. **Процесс отправки**:
   * `SendNotificationService` создает запись в таблице OrderStatus
   * `NotificationSender` (BackgroundService) периодически проверяет неотправленные сообщения
   * При успешной отправке в Kafka, сообщение помечается как отправленное

### Inbox Pattern

1. **Структура Inbox**:
   * Таблица `Notification` в базе данных для хранения входящих сообщений
   * Поля: Id, Payload (JSON), IsProcessed, CreatedAt

2. **Процесс получения**:
   * `NotificationConsumer` (BackgroundService) слушает Kafka топик
   * Полученные сообщения сохраняются в таблицу Notification
   * `NotificationProcessor` (BackgroundService) обрабатывает сообщения и обновляет статус

### Взаимодействие сервисов

1. **OrdersService → PaymentsService**:
   * При создании заказа, OrdersService отправляет уведомление через Outbox
   * PaymentsService получает уведомление через Inbox и обрабатывает платеж

2. **PaymentsService → OrdersService**:
   * После обработки платежа, PaymentsService отправляет уведомление через Outbox
   * OrdersService получает уведомление через Inbox и обновляет статус заказа

## Технологии

* .NET 8.0
* Entity Framework Core
* PostgreSQL
* Apache Kafka
* Docker

## Запуск проекта

1. Запустить Kafka и PostgreSQL через docker-compose:
```bash
docker-compose up -d
```

2. Запустить OrdersService и PaymentsService
3. API будет доступно через Swagger UI 