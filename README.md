# DemoApplication
Test Task
Разработано демо-приложение, включающее в себя форму регистрации пользователя и БД для хранения данных о проведенных регистрациях.

Для разработки использовались .Net Core 3.1 Core + Razor + JQuery + bootstrap.

Форма регистрации включает следующие поля:
<ul>
<li>Логин</li>
<li>Пароль с подтверждением</li>
<li>ФИО пользователя</li>
<li>Дата рождения</li>
<li>Адрес электронной почты</li>
<li>Номер телефона.</li>
</ul>

Присутствовует проверка логина на занятость в уже созданных записях.

Дата рождения больше или равна текущей дате.

Все поля обязательны к заполнению.

Адрес электронной почты и номер телефона проходят валидацию.

После регистрации есть возможность просмотра всего списка зарегистрировавшихся пользователей с возможностью редактирования, удаления и добавления новых записей.

Перед запуском проекта надо выполнить создание базы данных командой database-update.

Для разработки не использовалась библиотека Microsoft Identity с целью проработки всех этапов самостоятельно.
