Написать отчет, в котором отразить:
a. Общую идею вашего решения (какой функционал реализовали, особенно
если вносили изменения в функциональные требования).
b. Опишите какие принципы из SOLID и GRASP вы реализовали, скажите в
каких классах (модулях).
c. Опишите какие паттерны GoF вы реализовали, обоснуйте их важность,
скажите в каких классах (модулях) они реализованы.

Я сначала пытался проектировать, а потом писать проект, но за счет сериализации через файлы, я неправильно написал парсер, и ограничил тем самым себя в способах реализации. Сейчас парсер был переписан, и он больше не зависит от типов данных, но тем не менее конструкция уже была написана.

А именно: 
класс BankDataManager хранит в себе BankDataRepository, который хранит списки данных, и производит над ним операции такие как добавление, удаление и изменение данных. Получается, что BankManager реализует паттерн фасад.

Такие операции реализуются через команды, соответсвенно AddCommand, DeleteCommand и ChangeCommand. Получается, что пользователь вызывает эти команды, а они уже делают свое действие. И это покрывает основной функционал. Команды шаблонные, и вызывают соответствующий им шаблонный метод в BankDataManager.

Импорт и Экспорт, как я сказал, происходит через парсер, который был переписан в шаблонные классы. Сами парсеры используют TextReader, TextWriter, и пишут в нужном формате туда. 
Также есть классы-адаптеры:
Один класс вызывает шаблонный парсер используя стрим. 
Другой класс вызывает этот парсер стрима, используя файл
И еще один класс делает то же самое с консолью. 
Далее идет класс BankListParser, который в зависимости от параметров будет считывать список объектов либо из файла либо из консоли
И последний класс BankDataParser - считывает все данные банка из файлов, используя переданный парсер.

Еще одна проблема заключалась в том, что csv формат подразумевает несколько файлов, в то время как остальные форматы работают с одним файлом. Поэтому при переходе к шаблонным парсерам, я сделал так, чтобы каждый список данных содержался в отдельном файле. И названия для этих трех файлов автоматически генерировались. Пользователю достаточно ввести имя-ключ без расширения.

Проверка данных при вводе как от пользователя так и через импорт осуществляется автоматически, т.к. она встроена в BankDataManager. Из-за этого она поверхностная, и чтобы пересчитать балансы пользователей, необходимо использовать отдельную функцию.

Сами данные просто отражают задание, единственное что поменялось: тип категории - это эквивалент названия категории в задании. Потому что не ясно зачем "доход/расход" и в операции и в категории. Это бы создало дополнительные конфликты при импорте, и особо не влияет на логику.
Экземпляры создаются через фабрики. И добавляются уже через банк менеджер.

При импорте данных они записываются сначала в пустой менеджер, затем, если все получилось, данные уже сохраняются в основной экземпляр (в DI контейнере). Получается я реализовал прокси.

Анализ данных происходит в отдельном классе, который в свою очередь использует классы для фильтрации и сортировки. Это тоже фасад.

Статистика реализуется над функциями меню, которые в свою очередь тоже являются командами. Класс MenuCommandTime является декоратором над MenuItem и замеряет время выполнения функции, после чего сохраняет результат в класс StatisticsUI, который делает запись в файл statistics.json. В данный момент все меню обернуто в декораторы, и для вывода статистики есть отдельный пункт в глав меню.

На данный момент как я понимаю не реализовано только покрытие кода тестами.
