using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.UI.MenuUtils
{
    public static class MenuCommandBuilder
    {
        public static List<IMenuCommand> MenuItemList(List<KeyValuePair<string, UIFunc>> args)
        {
            var list = new List<IMenuCommand>();
            for (int i = 0; i < args.Count; i++)
            {
                list.Add(new MenuItem(args[i].Key, args[i].Value));
            }
            return list;
        }

        public static List<IMenuCommand> MenuCommandTimeList(List<KeyValuePair<string, UIFunc>> args)
        {
            var list = new List<IMenuCommand>();
            for (int i = 0; i < args.Count; i++)
            {
                list.Add(new MenuCommandTime(
                    new MenuItem(args[i].Key, args[i].Value))
                    );
            }
            return list;
        }

        public static List<IMenuCommand> MenuCommandTimeList(List<IMenuCommand> args)
        {
            var list = new List<IMenuCommand>();
            for (int i = 0; i < args.Count; i++)
            {
                list.Add(new MenuCommandTime( args[i] ));
            }
            return list;
        }
    }
}
