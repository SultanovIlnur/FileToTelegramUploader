# FTU - FileToTelegramUploader

CLI file uploader to Telegram groups. Supports manual uploads and automated scheduled uploads using cron (crontab).


How to use:
1. Create a bot and a Telegram group where you want to upload files. Add the bot to the Telegram group.
2. Execute `./ftu --filePath={Path to the file} --groupId={Telegram group ID} --botToken={Telegram bot token} --logResult={true|false}`

----
Консольна CLI программа для удобной выгрузки и публикации файлов в Телеграм группы. Поддерживается как ручная выгрузка, так и автоматическая с использованием crontab. 

Как использовать:
1. Создайте бота и Телеграм группу, в которую вы хотите выгружать файлы. Добавьте бота в нужную Телеграм группу.
2. Выполните команду `./ftu --filePath={Путь к файлу} --groupId={ID Телеграм группы} --botToken={Токен Телеграм бота} --logResult={true|false}`

