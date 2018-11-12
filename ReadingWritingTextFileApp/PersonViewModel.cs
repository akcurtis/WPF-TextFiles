using ReadingWritingTextFileApp.Helper_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace ReadingWritingTextFileApp
{
    public class PersonViewModel : ObservableObject
    {
        #region Fields

        private string _fName;
        private string _lName;
        private string _fullName;
        private int _age;
        private bool _isAlive;
        private bool _addUser = false;
        private bool _saveList;
        private string _selectedPerson;
        private ObservableCollection<string> _textFile = new ObservableCollection<string>();

        #endregion // Fields

        #region Properties

        public PersonViewModel()
        {

        }
    
        public string SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if(_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged(nameof(_selectedPerson));
                }
            }
        }


        public ObservableCollection<string> TextFile
        {
            get { return _textFile; }
            set
            {
                if (_textFile != value)
                {
                    _textFile = value;
                    OnPropertyChanged(nameof(TextFile));
                }
            }
        }


        public string FirstName
        {
            get { return _fName; }
            set
            {
                if (_fName != value)
                {
                    _fName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get { return _lName; }
            set
            {
                if (_lName != value)
                {
                    _lName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string FullName
        {
            get { return _fName + " " + _lName; }
            set
            {
                _fullName = value;
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                if (_isAlive != value)
                {
                    _isAlive = value;
                    OnPropertyChanged(nameof(IsAlive));
                }
            }
        }

        public bool AddUser
        {
            get { return _addUser; }
            set
            {
                if (_addUser != value)
                {
                    _addUser = value;
                    if (_addUser)
                    {
                        AddUserToList();
                    }

                    OnPropertyChanged(nameof(AddUser));
                }
            }
        }


        public bool SaveList
        {
            get { return _saveList; }
            set
            {
                if (_saveList != value)
                {
                    _saveList = value;
                    SaveListToFile();
                    OnPropertyChanged(nameof(SaveList));
                }
            }
        }

        #endregion // Properties


        #region Methods

        public void LoadTextFile()
        {
            _textFile = new ObservableCollection<string> ( File.ReadAllLines("personFile.txt"));
        }

        // Add User

        public void AddUserToList()
        {
            if (ValidateUserInput()) // check input valid before adding to list
            {
                _textFile.Add((FirstName + " " + LastName + " is " + Age + " and is " + IsAliveToText()));
                MessageBox.Show("User Added");
                ResetInput();
            }
            else
            {
                MessageBox.Show("Not a valid User");
            }
        }

        public string IsAliveToText()
        {
            return IsAlive ? "alive" : "dead";
        }

        public void DeleteUserFromList()
        {
            if(!string.IsNullOrEmpty(TextFile.ToString()))
            {
                TextFile.Remove(SelectedPerson);
                MessageBox.Show("User Deleted");
            }
        }

        public void ResetInput()
        {
            FirstName = "";
            LastName = "";
            Age = 0;
            IsAlive = false;
        }

        // Save List
        public void SaveListToFile()
        {
            string x = "";
            var sb = new StringBuilder();
            foreach(var people in _textFile)
            {
                x = sb.Append(people).ToString();
                sb.Append(Environment.NewLine);
            }
            File.WriteAllText("personFile.txt", x);
            MessageBox.Show("File Saved");
        }


        public bool ValidateUserInput()
        {
            // Error check for empty spaces(Trim) + length(string.Length()) 

            // Age Check
            if (Age <= 0 || Age > 100)
                return false;

            return (FirstName != null && LastName != null && Age != null);
        }

        #endregion // Methods



    }
}
