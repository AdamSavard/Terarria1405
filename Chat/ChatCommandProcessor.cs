// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.ChatCommandProcessor
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Chat.Commands;
using Terraria.Localization;

namespace Terraria.Chat
{
  public class ChatCommandProcessor : IChatProcessor
  {
    private readonly Dictionary<LocalizedText, ChatCommandId> _localizedCommands = new Dictionary<LocalizedText, ChatCommandId>();
    private readonly Dictionary<ChatCommandId, IChatCommand> _commands = new Dictionary<ChatCommandId, IChatCommand>();
    private readonly Dictionary<LocalizedText, NetworkText> _aliases = new Dictionary<LocalizedText, NetworkText>();
    private IChatCommand _defaultCommand;

    public ChatCommandProcessor AddCommand<T>() where T : IChatCommand, new()
    {
      string commandKey = "ChatCommand." + ((ChatCommandAttribute) AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>()).Name;
      ChatCommandId index = ChatCommandId.FromType<T>();
      this._commands[index] = (IChatCommand) new T();
      if (Language.Exists(commandKey))
      {
        this._localizedCommands.Add(Language.GetText(commandKey), index);
      }
      else
      {
        commandKey += "_";
        foreach (LocalizedText key in Language.FindAll((LanguageSearchFilter) ((key, text) => key.StartsWith(commandKey))))
          this._localizedCommands.Add(key, index);
      }
      return this;
    }

    public void AddAlias(LocalizedText text, NetworkText result)
    {
      this._aliases[text] = result;
    }

    public ChatCommandProcessor AddDefaultCommand<T>() where T : IChatCommand, new()
    {
      this.AddCommand<T>();
      this._defaultCommand = this._commands[ChatCommandId.FromType<T>()];
      return this;
    }

    private static bool HasLocalizedCommand(ChatMessage message, LocalizedText command)
    {
      string lower = message.Text.ToLower();
      string str = command.Value;
      if (!lower.StartsWith(str))
        return false;
      return lower.Length == str.Length || lower[str.Length] == ' ';
    }

    private static string RemoveCommandPrefix(string messageText, LocalizedText command)
    {
      string str = command.Value;
      return !messageText.StartsWith(str) || messageText.Length == str.Length || messageText[str.Length] != ' ' ? "" : messageText.Substring(str.Length + 1);
    }

    public ChatMessage CreateOutgoingMessage(string text)
    {
      ChatMessage message = new ChatMessage(text);
      KeyValuePair<LocalizedText, ChatCommandId> keyValuePair1 = this._localizedCommands.FirstOrDefault<KeyValuePair<LocalizedText, ChatCommandId>>((Func<KeyValuePair<LocalizedText, ChatCommandId>, bool>) (pair => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key)));
      ChatCommandId commandId = keyValuePair1.Value;
      if (keyValuePair1.Key != null)
      {
        message.SetCommand(commandId);
        message.Text = ChatCommandProcessor.RemoveCommandPrefix(message.Text, keyValuePair1.Key);
        this._commands[commandId].ProcessOutgoingMessage(message);
      }
      else
      {
        bool flag = false;
        for (KeyValuePair<LocalizedText, NetworkText> keyValuePair2 = this._aliases.FirstOrDefault<KeyValuePair<LocalizedText, NetworkText>>((Func<KeyValuePair<LocalizedText, NetworkText>, bool>) (pair => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key))); keyValuePair2.Key != null; keyValuePair2 = this._aliases.FirstOrDefault<KeyValuePair<LocalizedText, NetworkText>>((Func<KeyValuePair<LocalizedText, NetworkText>, bool>) (pair => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key))))
        {
          flag = true;
          message = new ChatMessage(keyValuePair2.Value.ToString());
        }
        if (flag)
          return this.CreateOutgoingMessage(message.Text);
      }
      return message;
    }

    public void ProcessIncomingMessage(ChatMessage message, int clientId)
    {
      IChatCommand chatCommand;
      if (this._commands.TryGetValue(message.CommandId, out chatCommand))
      {
        chatCommand.ProcessIncomingMessage(message.Text, (byte) clientId);
        message.Consume();
      }
      else
      {
        if (this._defaultCommand == null)
          return;
        this._defaultCommand.ProcessIncomingMessage(message.Text, (byte) clientId);
        message.Consume();
      }
    }
  }
}
