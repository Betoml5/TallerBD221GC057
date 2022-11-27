using Anime_CRUD.CRUD;
using Anime_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime_CRUD.ViewModels
{
    public class AnimeViewModel:INotifyPropertyChanged
    {
        ObservableCollection<Anime> Animes = new ObservableCollection<Anime>();
        AnimesContext context = new AnimesContext();
        AnimeCRUD crud = new AnimeCRUD();


        public string? Error { get; set; }
        public Anime? Anime { get; set; }

        public string View { get; set; } = "home";

        public AnimeViewModel()
        {
            var animes = crud.Read();
            foreach (var anime in animes)
            {
                Animes.Add(anime);
            }
            
        }

        public void Create(Anime Anime)
        {
            if (crud.Validate(Anime, out List<string> errors))
            {
                crud.Create(Anime);
                Animes.Add(Anime);
                Anime = null;
            }
            else
            {
                Error = string.Join(Environment.NewLine, errors);
            }

            PropertyChange();
        }

        public void Update(Anime Anime)
        {
            if (crud.Validate(Anime, out List<string> errors))
            {
                crud.Update(Anime);
                Anime = null;
            }
            else
            {
                Error = string.Join(Environment.NewLine, errors);
            }
            PropertyChange();
        }

        public void Delete(Anime Anime)
        {
            crud.Delete(Anime);
            Animes.Remove(Anime);
            Anime = null;
            PropertyChange();
        }

        public void UpdateDB()
        {
            Animes.Clear();
            foreach (var item in crud.Read())
            {
                Animes.Add(item);
            }

            PropertyChange();
        }


        public void ChangeView(string View)
        {
            this.View = View;
            PropertyChange();
        }
        void PropertyChange(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;


    }
}
