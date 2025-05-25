# HseAntiPlag

## Структура

## AntiPlag (Router)

**Router** выступает в роли гейтвея: он принимает запросы от клиента и проксирует их в соответствующий сервис (FileStoringService или FileAnalisysService).

### Состав:

* **Controllers**: FileAnalyzeController, FileStoringController, HseAntiplagController
* **Application**: фасады FileStoringFacade, FileAnalisysFacade
* **Infrastructure**: ApiConnectionService
* **Models**: DTO для передачи данных (FileDTO)

**Особенности:**

* Не содержит бизнес-логики
* Автоматически возвращает клиенту ответ от микросервиса в оригинальном формате (json, text, file)

---

## FileStoringService

**FileStoringService** отвечает за сохранение, поиск и учёт файлов пользователей. Реализует хранение с использованием PostgreSQL.

### Состав:

* **Controllers**: FileUploadController
* **Models**: UserFile, FileDTO, IUserFileRepository
* **Services**: FileFindService, FileToHashService, UserFileCheckService, UserFileFindService, UserFileService
* **Infrastructure**: UserFileFacade, FileContentsFacade, UserFileCheckFacade
* **DataBase**: FileSavingService

**Функции:**

* Принимает файлы от пользователей
* Сохраняет метаданные и содержимое в БД
* Вычисляет хеш-функцию файла для поиска дубликатов
* Проверяет наличие полного совпадения среди ранее загруженных файлов

**Архитектурно:** разделение слоёв (контроллеры → фасады → сервисы → модели/репозитории), без прямых зависимостей сверху вниз.

---

## FileAnalisysService

**FileAnalisysService** реализует анализ содержимого .txt-файлов: подсчёт слов, абзацев, символов, а также сравнение с другими файлами для обнаружения 100% схожести.

### Состав:

* **Controllers**: FileAnalyseController
* **Application**: AnalisysResultFacade
* **Services**: FileAnalyseService, FindAnalisysResultService
* **Models**: FileStatistics, FileCompare, FileContents
* **Infrastructure**: FileContentsService

**Функции:**

* Подсчёт статистики по тексту: количество слов, абзацев, символов
* Вычисление хеша и сравнение с другими файлами
* Возможность дополнить визуализацией (облако слов)

**Архитектура:** модульный сервис без состояния, с простой передачей строки текста на вход через API, и результатом анализа в виде JSON.

---

## Реализация требований

1. **Подсчет статистики** — реализован в `FileAnalisysService`
2. **Сравнение файлов** — реализовано в `FileAnalisysService`
3. **Микросервисная архитектура** — `FileStoringService` и `FileAnalisysService` работают независимо и могут быть развёрнуты отдельно. `AntiPlag` (router) обеспечивает объединённый API для пользователя.

---
## Swagger
Здесь показаны реализованные ручки
### FileAnalyze
- GET /statistics/{id} - получение статистики файла
- GET /compare/{id1}/{id2} - сравнение файлов
### FileStoring
- GET /files - список файлов
- GET /file/{id} - содержание файла
- POST /file - загрузить новый файл
