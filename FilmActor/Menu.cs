using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FilmActor
{
    internal class Menu
    {

        public List<string> MenuList;
        public int MenuSelect;
        public Printer Printer;
        public Boolean SelectedOption;
        public Menu()
        {
            StartingMenu();
            Printer = new Printer();
        }
        #region Updating Menu List
        private void ClearOptions()
        {
            if (MenuList != null)
            {
                MenuList.Clear();
            }
        }
        public void StartingMenu()
        {
            ClearOptions();
            MenuList = new List<string>()
            {
                "Display all actors",
                "Find films by actor",
                "Display all films",
                "Exit"
            };
        }
        public void FindActorByXOptions()
        {
            ClearOptions();
            MenuList = new List<string>()
            {
                "Search by full name",
                "Search by first name",
                "Search by last name"
            };
        }
        public void MenuUpdate(List<string> menuOptions)
        {
            MenuList = menuOptions;
        }
        #endregion
        #region MenuSelection
        public int MenuSelectionNormal()
        {
            MenuSelect = 0;
            SelectedOption = false;
            while (!SelectedOption)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Printer.PrintMenuNormal(this);
                ReadingMenuInput();
            }
            return MenuSelect;
        }
        #endregion
        #region User-input
        public void ReadingMenuInput()
        {
            var keyPressed = Console.ReadKey();

            switch (keyPressed.Key)
            {
                case ConsoleKey.DownArrow:
                    //when pressing down -> add+1 -> residuals of new value and menu.count
                    //if menu.count is bigger than value -> full new value is always the residual
                    //if menu.count is equal to value -> the new value becomes 0 (loop back to beginning)
                    MenuSelect = (MenuSelect + 1) % MenuList.Count;
                    break;
                case ConsoleKey.UpArrow:
                    //when pressing up -> if start of menu-option -> loop to last menu-option -> ELSE go up
                    if (MenuSelect == 0)
                    {
                        MenuSelect = MenuList.Count - 1;
                    }
                    else
                    {
                        MenuSelect--;
                    }
                    break;
                //when pressing enter -> update property to true -> break the loop
                case ConsoleKey.Enter:
                    SelectedOption = true;
                    break;
            }
        }
        #endregion
    }
}
