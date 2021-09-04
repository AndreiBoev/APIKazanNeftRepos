# API 
## Описание
Это приложение разработано для серверной части проекта KazanNeft. 

Чтобы получить информацию об активах, отделах, группам активов, необходимо использовать web-
API.

Root URL:

http://www.kazanneft.somee.com/
## Процедуры и функции:
### Просмотр активов:
Возвращает список отелей в формате JSON.

URL:

api/Assets 

Метод: GET

URL-параметры: Нет

Пример удачного ответа: [{"ID":1,"AssetSN":"05/04/0001","AssetName":"Toyota Hilux FAF321","DepartmentID":5,"WarrantyDate":null},{"ID":2,"AssetSN":"04/03/0001","AssetName":"Suction Line 852","DepartmentID":4,"WarrantyDate":"2020-03-02T00:00:00"},{"ID":3,"AssetSN":"01/01/0001","AssetName":"ZENY 3,5CFM Single-Stage 5 Pa Rotary Vane","DepartmentID":1,"WarrantyDate":"2018-01-17T00:00:00"},{"ID":4,"AssetSN":"05/04/0002","AssetName":"Volvo FH16","DepartmentID":5,"WarrantyDate":null}]
### Получение списка отделов:
Возвращает список отделов в формате JSON.

URL:
api/Departments

Метод: GET

URL-параметры: Нет

Пример удачного ответа: [{"Id":1,"Name":"Exploration"},{"Id":2,"Name":"Production"},{"Id":3,"Name":"Transportation"},{"Id":4,"Name":"R&D"},{"Id":5,"Name":"Distribution"},{"Id":6,"Name":"QHSE"}]
### Получение списка групп активов:

Возвращает список отделов в формате JSON.

URL:

api/AssetGroups

Метод: GET

URL-параметры: Нет

Пример удачного ответа: [{"Id":1,"Name":"Hydraulic"},{"Id":3,"Name":"Electrical"},{"Id":4,"Name":"Mechanical "}]
