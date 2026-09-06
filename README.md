---

# 📥 Download do jogo

Para jogar, você não precisa instalar o Visual Studio ou abrir o código-fonte.

O repositório possui uma pasta chamada **DownloadJogo**, que contém a versão já publicada do jogo.

## 1. Acesse o repositório

Abra a página do projeto no GitHub.

Na página principal do repositório, localize a pasta:

DownloadJogo


# ♟️ Xadrez

Aplicação desktop de xadrez desenvolvida em C# com Windows Forms, com foco na implementação das principais regras do jogo, gerenciamento de turnos entre dois jogadores e separação entre a lógica do jogo e a interface gráfica.

O projeto foi desenvolvido com uma arquitetura organizada para evitar que a interface visual seja responsável diretamente pelas regras do xadrez. O estado real da partida é mantido em um tabuleiro lógico, enquanto o Windows Forms atua apenas como camada de interação e representação visual.

---

## 📌 Sobre o projeto

O **Xadrez** é um jogo de tabuleiro para dois jogadores desenvolvido para desktop.

O sistema permite que os jogadores escolham suas respectivas cores antes do início da partida e realizem os movimentos seguindo as regras tradicionais do xadrez.

A aplicação possui:

- Tabuleiro 8x8;
- Dois jogadores;
- Escolha de cores;
- Controle de turnos;
- Movimentação das peças;
- Captura de peças;
- Verificação de movimentos legais;
- Proteção do próprio Rei;
- Xeque;
- Xeque-mate;
- Contagem de peças capturadas;
- Encerramento automático da partida;

---

## 🎯 Objetivos

O principal objetivo do projeto é desenvolver uma aplicação de xadrez funcional utilizando C# e Windows Forms, aplicando conceitos de:

- Programação Orientada a Objetos;
- Separação de responsabilidades;
- Modelagem de objetos;
- Estruturas de dados;
- Validação de regras;
- Manipulação de eventos;
- Gerenciamento de estado;
- Desenvolvimento de aplicações desktop;
- Organização de código;
- Arquitetura em camadas.

Além da implementação do jogo, o projeto busca manter a lógica do xadrez independente da interface gráfica.

---

## 🛠️ Tecnologias utilizadas

- **C#**
- **.NET**
- **Windows Forms**
- **Programação Orientada a Objetos**
- **LINQ**
- **Visual Studio**

---

## ♟️ Peças

O jogo possui todas as peças tradicionais do xadrez:

| Peça | Movimento |
|---|---|
| ♙ Peão | Move-se para frente e captura diagonalmente |
| ♖ Torre | Move-se horizontalmente ou verticalmente |
| ♘ Cavalo | Move-se em "L" |
| ♗ Bispo | Move-se pelas diagonais |
| ♕ Rainha | Move-se em qualquer direção |
| ♔ Rei | Move-se uma casa em qualquer direção |

As regras de movimentação são verificadas pelo mecanismo de regras do jogo antes que qualquer alteração seja realizada no tabuleiro lógico.

---

## 🎮 Funcionamento

### 1. Escolha dos jogadores

Antes de iniciar uma partida, os jogadores podem definir qual deles ficará com as peças brancas ou pretas.

A aplicação impede que os dois jogadores recebam a mesma cor.

A orientação visual do tabuleiro também é ajustada de acordo com o jogador que possui as peças brancas.

Quando:

```text
P1 = Brancas
P2 = Pretas
