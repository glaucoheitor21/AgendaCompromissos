# Agenda de Compromissos (Console App)

Este é um aplicativo de agenda de compromissos desenvolvido em C# (.NET 8, C# 12) para uso em linha de comando. O objetivo é permitir o registro e a consulta de compromissos considerando diferentes fusos horários, facilitando a organização de eventos em contextos internacionais.

## Grupo

- Glauco Heitor Gonçalves - RM 555978
- Pedro Henrique Junqueira - RM 556278

## Funcionalidades

- **Adicionar compromisso:**  
  Permite cadastrar um compromisso informando descrição, data/hora e fuso horário (timezone) escolhido a partir de uma lista dos principais do mundo.

- **Exibir compromissos do dia atual:**  
  Mostra todos os compromissos agendados para o dia atual, convertendo as datas para o timezone selecionado pelo usuário.

- **Exibir compromissos de uma data específica:**  
  Permite consultar compromissos de qualquer data, também considerando o timezone escolhido.

## Como usar

1. Execute o aplicativo pelo terminal ou pelo Visual Studio.
2. Escolha uma das opções do menu:
   - `1` para adicionar um compromisso
   - `2` para exibir compromissos do dia atual
   - `3` para exibir compromissos de uma data específica
   - `0` para sair
3. Ao adicionar ou consultar compromissos, selecione o timezone desejado a partir da lista apresentada.

## Principais Timezones Disponíveis

- UTC
- America/Sao_Paulo
- America/New_York
- America/Los_Angeles
- Europe/London
- Europe/Paris
- Europe/Berlin
- Asia/Tokyo
- Asia/Shanghai
- Asia/Kolkata
- Australia/Sydney
- Africa/Johannesburg
- America/Mexico_City
- America/Argentina/Buenos_Aires
- Pacific/Auckland

> **Observação:** Os IDs de timezone são mapeados para os reconhecidos pelo sistema operacional Windows. Caso utilize outro sistema operacional, pode ser necessário ajustar os IDs no código.
