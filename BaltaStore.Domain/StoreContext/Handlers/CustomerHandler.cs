using System;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Domain.StoreContext.ValueObjects;
using BaltaStore.Shared.Commands;
using FluentValidator;

namespace BaltaStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable,
        ICommandHandler<CreateCustomerCommand>,
        ICommandHandler<AddAddressCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;
        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handler(CreateCustomerCommand command)
        {
            // verificar se o CPF ja existe na base
            if (_repository.CheckDocument(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            // verificar se o E-mail ja existe na base
            if (_repository.CheckEmail(command.Email))
                AddNotification("Email", "Este E-mail ja esta em uso");

            // criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            // criar a entidade
            var customer = new Customer(name, document, email, command.Phone);

            // validar entidades e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo",
                Notifications
                );
            // persistir o cliente
            _repository.Save(customer);

            // enviar um e-mail de boas vindas
            _emailService.Send(email.Address, "hello@balta.io", "Bem vindo", "Seja bem vindo ao Balta Store!");

            // retornar o resultado para a tela

            return new CommandResult(true, "Bem Vindo ao balta store", new
            {
                Id = customer.Id,
                Name = name.ToString(),
                Email = email.Address
            });
        }

        public ICommandResult Handler(AddAddressCommand command)
        {
            throw new NotImplementedException();
        }

    }
}