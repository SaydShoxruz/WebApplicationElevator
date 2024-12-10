# WebApplicationElevator

WebApplicationElevator — это проект, который симулирует управление лифтом. 
API обрабатывает запросы на вызов лифта на определенный этаж, так же можно посмотреть текущие запросы на лифт, и этаж, в котором находится лифт.

Этот проект написан на ASP.NET Core и упакован в Docker для простого развертывания и имеется в DockerHub под название sshoxruz/web_application_elevator

## Требования

Перед запуском убедитесь, что у вас установлены:

- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/)

## Запуск через Docker

Клонируйте репозиторий:
```bash
   git clone https://github.com/yourusername/elevator-webapi.git
   cd elevator-webapi
```
Соберите Docker-образ и запустите контейнер:
```bash
docker-compose up --build -d
```
Чтобы остановить контейнеры:
```bash
docker-compose down
```

### 4. **Использование API**

```markdown
POST
http://localhost:5263/api/Elevator/CallTheElevator
```
Вызывает лифт или же, если лифт занят, ставит в очередь, где по приоритету обрабатываются запросы. Пример тела запроса:
  ```json
  {
    "currentFloor": 1,
    "targetFloor": 5
  }
```
```markdown
GET
http://localhost:5263/api/Elevator/GetAllRequests
```
Возвращает текущие запросы на лифт

```markdown
GET
http://localhost:5263/api/Elevator/GetElevatorCurrentFloor
```
Возвращает текущий этаж лифта

### **Контакты**

```markdown

## Контакты

Если у вас возникли вопросы или предложения, свяжитесь со мной:

- GitHub: [SaydShoxruz](https://github.com/SaydShoxruz)
- Email: saydshox123@gmail.com
