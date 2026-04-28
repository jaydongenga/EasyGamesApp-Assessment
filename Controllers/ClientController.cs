using EasyGamesApp.Data;
using EasyGamesApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyGamesApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly TransactionRepository _repo;

        public ClientController(TransactionRepository repo)
        {
            _repo = repo;
        }

        // ── Main page: show client list + selected client's transactions ───────
        public IActionResult Index(int? clientID, string? searchTerm, string sortOrder = "name_asc")
        {
            var vm = new ClientViewModel
            {
                Clients = _repo.GetClients(searchTerm, sortOrder).ToList(),
                TransactionTypes = _repo.GetTransactionTypes().ToList(),
                SearchTerm = searchTerm,
                SortOrder = sortOrder
            };

            if (clientID.HasValue && clientID > 0)
            {
                vm.SelectedClientID = clientID.Value;
                vm.SelectedClient = _repo.GetClientByID(clientID.Value);
                vm.Transactions = _repo.GetTransactionsByClient(clientID.Value).ToList();
            }

            return View(vm);
        }

        // ── Add a new transaction (POST) ──────────────────────────────────────
        [HttpPost]
        public IActionResult AddTransaction(int clientID, decimal amount,
                                            short transactionTypeID, string? comment,
                                            string? searchTerm, string sortOrder = "name_asc")
        {
            if (clientID > 0 && amount != 0)
            {
                _repo.AddTransaction(amount, transactionTypeID, clientID, comment);
            }

            return RedirectToAction("Index", new { clientID, searchTerm, sortOrder });
        }

        // ── Update a transaction comment (POST) ───────────────────────────────
        [HttpPost]
        public IActionResult UpdateComment(long transactionID, string comment,
                                           int clientID, string? searchTerm,
                                           string sortOrder = "name_asc")
        {
            _repo.UpdateComment(transactionID, comment ?? "");
            return RedirectToAction("Index", new { clientID, searchTerm, sortOrder });
        }
    }
}