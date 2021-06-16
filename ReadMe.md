# Описание шаблонного решения Simple Social Network Parser

## Начало использования - получения токена VK
1. Создайте приложение и получите его ИД (при переходе в настройки приложения видно в строке поиска браузера)
2. По инструкции https://vk.com/dev/implicit_flow_group, формируем строку-запрос, вместо <ИД ПРИЛОЖЕНИЯ> укажите ИД вашего приложения https://oauth.vk.com/authorize?client_id=<ИД ПРИЛОЖЕНИЯ>&display=page&scope=NewsFeed&response_type=token&v=5.131&state=success
3. Получаем токен в строке браузера и запоминаем его.

## Сохраняет токен в библиотеке Secret
1. Откройте в Visual Studio или Visual Code проект SimpleSocialNetworkParser\Secret\Secret.sln.
2. В строке public static string VkToken = ""; // Insert your VK token. укажите Ваш токен.
3. Соберите библиотеку. Библиотеку необходимо подключить в проекте SimpleSocialNetworkParser.
Храним таким образом, чтобы нельзя было прочитать токен. Он будет внутри библиотеки.

## Сборка инструмента
Соберите проект SimpleSocialNetworkParser\SimpleSocialNetworkParser\SimpleSocialNetworkParser.sln
Решение готово к использованию.

Пример использования:
DEV\SimpleSocialNetworkParser\SimpleSocialNetworkParser\SimpleSocialNetworkParser\bin\Debug\netcoreapp3.1>SimpleSocialNetworkParser.exe -n vk -a get -q "#indiegame" -s "2021-06-14" -e "2021-06-16" -x csv

Результат будет записан в теукщую папку в файл формата csv.

Решение реализовано с точками расширения функциональности.

Также есть справка

    -n, --network=VALUE        Тип социальной сети. vk
    -a, --action=VALUE         Действие. Get или Send
    -q, --query=VALUE          Запрос поиска. Если надо найти сообщения по
                               хештегу, то указать хештеги со знаком #
    -s, --startDate=VALUE      Дата начала поиска в формате yyyy-MM-dd.
    -e, --endDate=VALUE        Конечная дата поиска в формате yyyy-MM-dd.
    -x, --exportVariant=VALUE  Вариант экспорта данных. csv
    -h, --help                 Показать справку.

Вызвается при запуске приложения без параметра, либо с параметром -h.

Раюотает как в ОС Windows, так и в Linux (.net core 3.1).
