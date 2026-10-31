# Medical Information System

Тестовый backend для медицинской информационной системы на .NET 10 и .NET Aspire.

## Что используется

- ASP.NET Core Web API;
- Entity Framework Core;
- PostgreSQL;
- .NET Aspire;
- OpenTelemetry.

Проект разделён на Domain, Application, Infrastructure и API. В базе есть пациенты, доктора, болезни и диагнозы.

## Запуск

Для запуска нужны .NET 10 SDK и Docker Desktop. Сначала нужно запустить Docker Desktop, затем из корня проекта выполнить:

```bash
dotnet restore
dotnet run --project src/MedicalInformationSystem.AppHost
```

Aspire поднимет API и PostgreSQL. При первом запуске применятся миграции и добавятся тестовые данные. Ссылка на Aspire Dashboard появится в терминале. В Dashboard нужно открыть ресурс `api` и перейти по его адресу.

Остановить приложение можно сочетанием `Ctrl+C`. Данные PostgreSQL сохранятся в Docker volume.

## Маршруты

| Метод | Маршрут | Описание |
| --- | --- | --- |
| `GET` | `/api/patients` | Все пациенты |
| `GET` | `/api/patients/{id}` | Информация о пациенте и его диагнозах |
| `GET` | `/api/doctors` | Все доктора |
| `GET` | `/api/doctors?specialty=Кардиолог` | Доктора по специальности |
| `GET` | `/api/doctors/specialties` | Список специальностей |
| `GET` | `/api/diseases` | Список болезней |
| `GET` | `/health` | Проверка состояния API |

Примеры запросов находятся в `MedicalInformationSystem.http`. Перед выполнением нужно указать актуальный адрес API в переменной `apiUrl`.

## Миграции

Начальная миграция уже добавлена и применяется автоматически при запуске.

Создание новой миграции:

```bash
dotnet tool restore
dotnet ef migrations add MigrationName \
  --project src/MedicalInformationSystem.Infrastructure \
  --startup-project src/MedicalInformationSystem.Api \
  --output-dir Persistence/Migrations
```

Сборка решения:

```bash
dotnet build MedicalInformationSystem.sln
```

## Тесты

Перед запуском тестов Docker Desktop должен быть включён.

```bash
dotnet test MedicalInformationSystem.sln
```

Тесты запускают отдельный PostgreSQL без постоянного volume, применяют миграцию и проверяют API реальными HTTP-запросами.

Ошибки возвращаются в формате `ProblemDetails`. Основные действия и необработанные исключения записываются в логи.
