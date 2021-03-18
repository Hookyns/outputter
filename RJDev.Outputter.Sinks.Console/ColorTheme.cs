// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Colorify.UI;
//
// namespace RJDev.Tyml.Core.Output
// {
// 	public class ColorTheme : IEnumerable
// 	{
// 		public static readonly ColorTheme DarkConsole = new()
// 		{
// 			{ MessageType.General, ConsoleColor.White }
// 		};
// 		
// 		public static readonly ColorTheme LightConsole = new()
// 		{
//
// 		};
//
// 		/// <summary>
// 		/// Dictionary of colors
// 		/// </summary>
// 		private readonly IDictionary<MessageType, Color> colors = new Dictionary<MessageType, Color>(); 
//
// 		/// <summary>
// 		/// Add message type color
// 		/// </summary>
// 		/// <param name="messageType"></param>
// 		/// <param name="color"></param>
// 		public void Add(MessageType messageType, ConsoleColor color)
// 		{
// 			this.colors.Add(messageType, color);
// 		}
//
// 		/// <inheritdoc />
// 		public IEnumerator GetEnumerator()
// 		{
// 			return this.colors.GetEnumerator();
// 		}
// 	}
// }