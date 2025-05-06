# OldPhonePadDecoder

This project simulates the behavior of an old mobile phone keypad, converting a sequence of keypresses into text. Each number key (2–9) maps to a set of characters, and pressing the same key repeatedly cycles through those characters. A pause (`space`) is required when entering two characters from the same key in sequence.

## 🔢 Input Format

- Digits `2` to `9` represent keypad characters (e.g., `2` → A, `22` → B).
- A **space** indicates a pause to enter another letter from the same key.
- `*` represents **backspace**, deleting the last confirmed character.
- `#` signals the **end** of input and finalizes the output.

## ✨ Example

**Input:**  
`8 88777444666*664#`

**Breakdown:**  
- `8` → T  
- `88` → U  
- `777` → R  
- `444` → I  
- `666` → O  
- `*` → delete O  
- `66` → N  
- `4` → G  
- `#` → submit

**Output:**  
`TURING`

## ✅ Features

- Supports multi-press character cycling (like real keypads)
- Handles backspace logic
- Throws Invalid operation exception on unmapped digits (e.g., 1)
- Halts processing at the first `#` character
- Throws ArgumentException on empty, whitespace or null input

## 🧪 Unit Tests

Tests include:
- Basic decoding
- Pauses between the same key
- Backspace removal
- Invalid digit handling
- Early termination on `#`

## 🔧 Tech Stack

- C#
- .NET 8 LTS (Console App)
- xUnit for testing

## 🚀 How to Run

1. Clone the repository
2. Open in Visual Studio or `dotnet run`
3. Run tests via `dotnet test`
