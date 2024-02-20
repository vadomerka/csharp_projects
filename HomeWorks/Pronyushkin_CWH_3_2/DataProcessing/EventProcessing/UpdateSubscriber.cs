using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.EventProcessing
{
    /// <summary>
    /// Класс для связывания событий и их подписчиков.
    /// </summary>
    public static class UpdateSubscriber
    {
        /// <summary>
        /// Подписывает экземпляр AutoSaver на события объектов.
        /// </summary>
        /// <param name="objects">Список объектов</param>
        /// <param name="aSaver">Экземпляр AutoSaver</param>
        /// <exception cref="ArgumentNullException">Выбрасывает ошибку при пустом AutoSaver</exception>
        public static void UpdateSubscribe(IEnumerable<IUpdate> objects, AutoSaver aSaver)
        {
            if (aSaver == null) throw new ArgumentNullException();
            foreach (IUpdate obj in objects)
            {
                obj.Updated += aSaver.UpdateSave;
            }
        }
    }
}
