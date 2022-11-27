﻿using Anime_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime_CRUD.CRUD
{
    public class AnimeCRUD
    {

        AnimesContext context = new();

        public bool Validate(Anime Anime, out List<string> Errors)
        {

            List<string> errors = new();

            if (string.IsNullOrWhiteSpace(Anime.Nombre))
            {
                errors.Add("El nombre del anime no puede estar vacio");
            }

            if (Anime.Capitulos <= 0)
            {
                errors.Add("El anime debe tener al menos un capitulo");
            }

            if (string.IsNullOrWhiteSpace(Anime.Autor))
            {
                errors.Add("El anime debe tener un autor");
            }

            Errors = errors;

            return errors.Count == 0;

        }


        public void Create(Anime anime)
        {
            context.Anime.Add(anime);
            context.SaveChanges();
        }

        public IEnumerable<Anime> Read()
        {
            return context.Anime.ToList();
        }

        public void ReadOne()
        {



        }

        public void Update(Anime anime)
        {
            context.Anime.Update(anime);
            context.SaveChanges();
        }

        public void Delete(Anime anime)
        {
            context.Anime.Remove(anime);
            context.SaveChanges();
        }
    }
}
