using Anime_CRUD.CRUD;
using Anime_CRUD.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anime_CRUD.ViewModels
{
    public class AnimeViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Anime> Animes { get; set; } = new ObservableCollection<Anime>();
        AnimesContext context = new AnimesContext();
        AnimeCRUD crud = new AnimeCRUD();


        public string? Error { get; set; }
        public Anime? Anime { get; set; }

        public string View { get; set; } = "home";

        public string SearchTerm { get; set; } 


        //Commands
        public ICommand ChangeViewCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand ViewAnimeDetailsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ResetAnimesCommand { get; set; }

        public ICommand FindByNameCommand { get; set; }

        public AnimeViewModel()
        {

            ChangeViewCommand = new RelayCommand<string>(ChangeView);
            CreateCommand = new RelayCommand(Create);
            ViewAnimeDetailsCommand = new RelayCommand<Anime>(ViewAnimeDetails);
            DeleteCommand = new RelayCommand(Delete);
            UpdateCommand = new RelayCommand(Update);
            FindByNameCommand = new RelayCommand(FindByName);
            ResetAnimesCommand = new RelayCommand(ResetAnimes);

            var animes = crud.Read();
            foreach (var anime in animes)
            {
                Animes.Add(anime);
            }
            
        }
        
        private void ResetAnimes()
        {
            Animes.Clear();
            var animes = crud.Read();
            foreach (var anime in animes)
            {
                Animes.Add(anime);
            }
        }

        public void FindByName()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var animes = crud.FindByName(SearchTerm);
                Animes.Clear();
                foreach (var anime in animes)
                {
                    Animes.Add(anime);
                }
            }
            

            PropertyChange();

        }

        public void ViewAnimeDetails(Anime Anime)
        {
            this.Anime = Anime;
            ChangeView("details");
        }
        
        public void Create()
        {
            if (crud.Validate(Anime, out List<string> errors))
            {
                crud.Create(Anime);
                UpdateDB();
                ChangeView("home");
                Anime = null;
            }
            else
            {
                Error = string.Join(Environment.NewLine, errors);
            }

            PropertyChange();
        }

        public void Update()
        {

            if (Anime != null)
            {
                if (crud.Validate(Anime, out List<string> errors))
                {
                    var animeExists = crud.ReadOne(Anime.Id);
                    if (animeExists != null)
                    {
                        animeExists.Id = Anime.Id;
                        animeExists.Nombre = Anime.Nombre;
                        animeExists.Autor = Anime.Autor;
                        animeExists.Imagen = Anime.Imagen;
                        animeExists.Capitulos = Anime.Capitulos;
                        crud.Update(animeExists);
                        Anime = null;
                        ChangeView("home");

                    }
                }
                else
                {
                    Error = string.Join(Environment.NewLine, errors);
                }
            }
            PropertyChange();
        }

        public void Delete()
        {
            crud.Delete(Anime);
            Anime = null;
            UpdateDB();
            ChangeView("home");
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
            Error = "";
            this.View = View;

            if (View == "create")
            {
                 Anime = new Anime();
            }
            if (View == "edit")
            {
                Anime clon = new Anime()
                {
                    Id = Anime.Id,
                    Autor = Anime.Autor,
                    Capitulos = Anime.Capitulos,
                    Imagen = Anime.Imagen,
                    Nombre = Anime.Nombre
                };
                Anime = clon;
            }
            
            PropertyChange();
        }
        void PropertyChange(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;


    }
}
