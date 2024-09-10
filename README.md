# FTU - FileToTelegramUploader

App for uploading files to Telegram groups. Both manual and automatic uploading using crontab are supported.


How to use:
1. Create a bot and a Telegram group where you want to upload files. Add the bot to the Telegram group.
2. Execute `./ftu --filePath={Path to the file} --groupId={Telegram group ID} --botToken={Telegram bot token} --logResult={true|false}`

----
Программа для выгрузки и публикации файлов в Телеграм группы. Поддерживается как ручная выгрузка, так и автоматическая при использовании crontab. 

Как использовать:
1. Создайте бота и Телеграм группу, в которую вы хотите выгружать файлы. Добавьте бота в нужную Телеграм группу.
2. Выполните команду `./ftu --filePath={Путь к файлу} --groupId={ID Телеграм группы} --botToken={Токен Телеграм бота} --logResult={true|false}`

