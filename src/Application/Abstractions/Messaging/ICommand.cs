namespace Application.Abstractions.Messaging;

public interface ICommand;  // will not sent response

public interface ICommand<TResponse>; // will sent response of type eg. guid or list<>
