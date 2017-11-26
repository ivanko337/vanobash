namespace Bash
{
    /// <summary>
    /// ОСТОРОЖНО
    /// КЛАСС СОСТОИТ ИЗ ДИЧАЙШИХ КОСТЫЛЕЙ
    /// ПРИ ИСПОЛЬЗОВАНИИ СОБЛЮДАТЬ ВСЕ ПРАВИЛА
    /// </summary>
    static class ServiceClass
    {
        /// <summary>
        /// Возвращает массив параметров
        /// </summary>
        /// <param name="line">Входная строка</param>
        /// <param name="command">Название команды</param>
        /// <returns>Массив параметров</returns>
        public static string[] GetParams(string line, ref string command)
        {
            // Определить количество параметров в строке
            int paramsCount = GetParamsCount(line);
            // Массив с параметрами
            string[] parameters = new string[paramsCount];

            // забыл для чего нужен, но без него не работает
            int index = GetCommandName(line, ref command);

            for (int i = 0; i < paramsCount; i++)
            {
                parameters[i] = GetParameter(line, ref index);
            }

            return parameters;
        }
        /// <summary>
        /// Возвращает название команды
        /// </summary>
        /// <param name="line">Входная строка из терминала</param>
        /// <param name="comandName">Название команды</param>
        /// <returns>Индекс, с которого начинаются параметры</returns>
        private static int GetCommandName(string line, ref string comandName)
        {
            int answer = 0;
            comandName = GetParameter(line, ref answer);
            return answer;
        }
        /// <summary>
        /// Возвращает один параметр из строки, начинаея с определённого символа
        /// </summary>
        /// <param name="line">Входная строка из терминала</param>
        /// <param name="index">Индекс символа, начиная с которого нужно считывать параметр.</param>
        /// <returns>Параметр от переданного индекса до следующего пробела</returns>
        private static string GetParameter(string line, ref int index)
        {
            string answer = "";


            // Небольной костылик в том, что передаваемый индекс - индекс пробела, поэтому
            // в коде метода присутствует прибавление единицы к индексу перед началом работы, а
            // проверка на ноль делается, чтобы не съедать первый символ названия команды
            if (index != 0)
                ++index;

            while (index + 1 <= line.Length)
            {
                answer += line[index];
                index++;
                if (index == line.Length)
                    break;
            }

            return answer;
        }
        private static int GetParamsCount(string line)
        {
            int answer = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ' && i != line.Length)
                    answer++;
            }
            return answer;
        }
    }
}
