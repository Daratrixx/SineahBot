using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SineahBot.Data
{
    public class Shop : INamed
    {
        private INamed owner;
        public void RegisterOwner(INamed owner)
        {
            this.owner = owner;
        }
        public List<ShopEntry> entries = new List<ShopEntry>();
        public IEnumerable<ShopEntry> GetBuyableEntries()
        {
            return entries.Where(x => x.goldCost.HasValue);
        }
        public IEnumerable<ShopEntry> GetSellableEntries()
        {
            return entries.Where(x => x.goldRefund.HasValue);
        }
        private List<Character> clients = new List<Character>();
        public void AddClient(Character client)
        {
            clients.Add(client);
            client.currentShop = this;
            client.characterStatus = CharacterStatus.Trade;
        }
        public void RemoveClient(Character client)
        {
            clients.Remove(client);
            client.currentShop = this;
            client.characterStatus = CharacterStatus.Normal;
        }
        public void CloseShop()
        {
            foreach (var client in clients)
            {
                client.characterStatus = CharacterStatus.Normal;
                client.currentShop = null;
                client.Message("The trade interrupted");
            }
        }
        public string GetName(IAgent agent = null)
        {
            return owner?.GetName(agent);
        }
        public Shop RegisterEntry(Item referenceItem, int? goldCost, int? goldRefund)
        {
            entries.Add(new ShopEntry()
            {
                referenceItem = referenceItem,
                goldCost = goldCost,
                goldRefund = goldRefund
            });
            return this;
        }

        public ShopEntry FindShopEntry(string entryName)
        {
            entryName = entryName.ToLower();
            var output = entries.FirstOrDefault(x => x.referenceItem.GetName().ToLower() == entryName);
            return output;
        }
    }
    public class ShopEntry : INamed
    {
        public Item referenceItem;
        public int? goldCost;
        public int? goldRefund;

        public string GetName(IAgent agent = null)
        {
            return ((INamed)referenceItem)?.GetName(agent);
        }
    }
}
