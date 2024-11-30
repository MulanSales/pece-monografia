for (int i = 0; i < players.Length; i ++)
{
sb.DrawString(font, "P" + (i + 1) + ": " + players[i].score, temp, Color.White);
temp.Y += 32;
}