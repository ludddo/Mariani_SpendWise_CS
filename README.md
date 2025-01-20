# Progetto di Implementazione BCrypt e Correzione del Login

Questo progetto ha l'obiettivo di implementare l'algoritmo di hashing BCrypt in C# e correggere il sistema di login esistente per utilizzare BCrypt per la gestione delle password.

## Requisiti

- .NET Core SDK
- Visual Studio o altro IDE compatibile con C#
- Libreria BCrypt per .NET

## Configurazione del Progetto

1. **Clona il repository**
    ```sh
    git clone https://github.com/tuo-utente/tuo-repository.git
    cd tuo-repository
    ```

2. **Installa la libreria BCrypt**
    Aggiungi il pacchetto BCrypt.Net-Next al progetto utilizzando NuGet.
    ```sh
    dotnet add package BCrypt.Net-Next
    ```

## Implementazione dell'Algoritmo BCrypt

### Passo 1: Hashing della Password
