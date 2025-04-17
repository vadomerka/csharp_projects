using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Identity.Client;
using System.Drawing;
using System.Net;

namespace HW_CPS_HSEZoo_2.Infrastructure
{
    public static class AnimalFacade
    {
        private static IServiceProvider services = CompositionRoot.Services;
        public static IEnclosable GetAnimal(int enId, int anId) {
            var ens = services.GetRequiredService<EnclosureDataService>();
            var ans = services.GetRequiredService<AnimalDataService>();
            return ans.GetAnimal(ens.GetEnclosure(enId), anId);
        }

        public static void AddAnimal(int enId, AnimalDTO dto)
        {
            var creation = services.GetRequiredService<AnimalDataService>();
            var enclosures = services.GetRequiredService<EnclosureDataService>();
            var moving = services.GetRequiredService<AnimalTransferService>();

            var animal = creation.Create(dto);
            var enclosure = enclosures.GetEnclosure(enId);

            moving.Add(enclosure, animal);
        }

        public static void DeleteAnimal(int enId, int anId)
        {
            var ans = services.GetRequiredService<AnimalDataService>();
            var ens = services.GetRequiredService<EnclosureDataService>();
            var delete = services.GetRequiredService<AnimalTransferService>();

            var enclosure = ens.GetEnclosure(enId);
            var animal = ans.GetAnimal(enclosure, anId);

            delete.Remove(enclosure, animal);
        }

        public static void MoveAnimal(int fromId, int anId, int toId)
        {
            var ans = services.GetRequiredService<AnimalDataService>();
            var ens = services.GetRequiredService<EnclosureDataService>();
            var move = services.GetRequiredService<AnimalTransferService>();

            var from = ens.GetEnclosure(fromId);
            var animal = ans.GetAnimal(from, anId);
            var to = ens.GetEnclosure(toId);

            move.Move(from, animal, to);
        }

        public static void FeedAnimal(int enId, int anId) 
        {
            var ans = services.GetRequiredService<AnimalDataService>();
            var ens = services.GetRequiredService<EnclosureDataService>();
            var fos = services.GetRequiredService<FeedingOrganizationService>();

            var animal = ans.GetAnimal(ens.GetEnclosure(enId), anId);
        }
    }
}
