namespace Les1
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new();
        private readonly Notification _notification = new() { SendDelay = 300 };

        public Restaurant()
        {
            for (byte i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        /// <summary>
        /// Бронирование стола 
        /// </summary>
        /// <param name="countOfPersons"></param>
        public void BookFreeTable(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду я подберу столик и подтвержу вашу бронь, оставайтесь на линии");

            var table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == StateTable.Free);

            Thread.Sleep(1000 * 5);

            Console.WriteLine(table is null
                ? "К сожалению, сейчас все столики заняты"
                : "Готово! Ваш столик номер " + table.Id);
        }

        /// <summary>
        /// Бронирование стола (Async)
        /// </summary>
        /// <param name="countOfPersons"></param>
        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду я подберу столик и подтвержу вашу бронь, вам придёт уведомление");

            Task.Run(async () =>
            {

                var table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == StateTable.Free);

                await Task.Delay(1000 * 5);
                table?.SetState(StateTable.Booked);


                await _notification.Send(table is null
                ? "К сожалению, сейчас все столики заняты"
                : "Готово! Ваш столик номер " + table.Id);
            });
        }

        /// <summary>
        /// Отмена брони стола
        /// </summary>
        /// <param name="id">Id Стола</param>
        public void FreeTable(int id)
        {
            Console.WriteLine("Добрый день! Подождите секунду я освобожу столик, оставайтесь на линии");
            var table = _tables.FirstOrDefault(t => t.Id == id);

            Thread.Sleep(1000 * 5);

            table?.SetState(StateTable.Free);

            Console.WriteLine(table is null
                ? "Такого столика нет в нашем ресторане"
                : "Готово! Мы отменили вашу бронь");
        }

        /// <summary>
        /// Отмена брони стола (Async)
        /// </summary>
        /// <param name="id">Id Стола</param>
        public void FreeTableAsync(int id)
        {
            Console.WriteLine("Добрый день! Подождите секунду я освобожу столик, вам придёт уведомление");

            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.Id == id && t.State == StateTable.Booked);

                await Task.Delay(1000 * 5);

                table?.SetState(StateTable.Free);

                await _notification.Send(table is null
                    ? "Такого столика нет в нашем ресторане"
                    : "Готово! Мы отменили вашу бронь");
            });
        }
    }
}
