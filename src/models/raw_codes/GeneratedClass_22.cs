using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GOL
{
class GOLHandler
{
//Fields
private Cell[,] ActualGeneration = new Cell[80, 60];
private Cell[,] NextGeneration = new Cell[80, 60];
List<Cell> AliveCells = new List<Cell>();
DispatcherTimer timer;
PlayerNameIntro Intro = new PlayerNameIntro();

//Event
public event EventHandler Timer_Ticked;

//Constructor
public GOLHandler()
{
int value = 300;
timer = new DispatcherTimer();
timer.Interval = TimeSpan.FromMilliseconds(value);
//timer.IsEnabled = true;
//timer.Stop();
timer.Tick += Timer_Tick;
}

//Tick handler that raises the timer_ticked event if it has subscribers.
private void Timer_Tick(object sender, EventArgs e)
{
//Check so the event have a subscriber.
if (Timer_Ticked != null)
{
Timer_Ticked.Invoke(this, new EventArgs());
}

}

/// <summary>
/// Method for adding new cells to the ActualGeneration.
/// </summary>
/// <param name="cell">Send the cell you want to add.</param>
public void AddCell(Cell cell)
{
ActualGeneration.SetValue(cell, cell.X, cell.Y);
}

/// <summary>
/// Send the coordinates from the position you click in the canvas and the radius of the square the cells is in.
/// This method will divide the coordinates by 10 and rounds it to nearest even 10.
/// </summary>
/// <param name="X_index">send The X coordinate.</param>
/// <param name="Y_index">Send The Y coordinate</param>
/// <param name="RadiusOfTheSquare">Send the Radius of the Square</param>
public void KillOrMakeCell(double X_index, double Y_index,int RadiusOfTheSquare)
{
//Casting the values to an int.
int X = (int)X_index;
int Y = (int)Y_index;

//Substract the radius value so it will be the center point.
X -= RadiusOfTheSquare;
Y -= RadiusOfTheSquare;

//Rounds it to the nearest 10.
X = ((int)Math.Round(X / 10.0));
Y = ((int)Math.Round(Y / 10.0));

//Kill or make the cell alive.
if (ActualGeneration[X, Y].IsAlive == true)
{
ActualGeneration[X, Y].IsAlive = false;
}
else
{
ActualGeneration[X, Y].IsAlive = true;
}
}


public void UpdateDatabase()
{
for (int i = 0; i < ActualGeneration.GetLength(0); i++)
{
for (int j = 0; j < ActualGeneration.GetLength(1); j++)
{
if (ActualGeneration[i, j].IsAlive)
{
var c = ActualGeneration[i, j];
AliveCells.Add(new Cell(c.X, c.Y, true));
}
}
}

using (GContext db = new GContext())
{
int maxGen;

if(db.Generations.Count() != 1)
{
maxGen = db.Generations.Max(p => p.GenNumber);
maxGen += 1;
}
else
{
maxGen = 1;
}


foreach (var item in AliveCells)
{
Generation gen = new Generation();

gen.GenNumber = maxGen;
gen.Cell_X = item.X;
gen.Cell_Y = item.Y;
db.Generations.Add(gen);
}
db.SaveChanges();
AliveCells.Clear();
}

}

/// <summary>
/// Method that looping through the ActualGenerations MultiDimensionalArray,
/// And Checks how many neighboor every cell has and then make the new generation.
/// </summary>
public void calculateNextGeneration()
{
for (int i = 0; i < ActualGeneration.GetLength(0); i++)
{
for (int j = 0; j < ActualGeneration.GetLength(1); j++)
{
//Method for count how many neighboor every cell has and then throw it into the switch.
int neighboors = CheckLivingNeighboors(i, j);

#region SwitchOnAllTheCells
switch (neighboors)
{
case 0:
{
NextGeneration.SetValue(new Cell(i, j), i, j);
break;
}
case 1:
{
NextGeneration.SetValue(new Cell(i, j), i, j);
break;
}
case 2:
{
if (ActualGeneration[i, j].IsAlive)
{
NextGeneration.SetValue(new Cell(i, j), i, j);
NextGeneration[i, j].IsAlive = true;
break;
}
else
{
NextGeneration.SetValue(new Cell(i, j), i, j);
}
break;
}
case 3:
{
NextGeneration.SetValue(new Cell(i, j), i, j);
NextGeneration[i, j].IsAlive = true;
break;
}
default:
{
NextGeneration.SetValue(new Cell(i, j), i, j);
break;
}
#endregion
}
}
}
}

/// <summary>
/// Method that returns the actualGeneration.
/// </summary>
/// <returns>Actual generation MultiDimensionalArray with the cells.</returns>
public Cell[,] GetActualGeneration()
{
return ActualGeneration;
}

/// <summary>
/// Start the timer, The interval is 2 seconds by default.
/// </summary>
public void Start_Timer()
{
timer.Start();
}

/// <summary>
/// Stops the timer.
/// </summary>
public void Stop_Timer()
{
timer.Stop();
}

/// <summary>
/// Method that returns the NextGeneration MultiDimensionalArrat with the cells.
/// </summary>
/// <returns></returns>
public Cell[,] GetNextGeneration()
{
return NextGeneration;
}

//Checks the surrounding Neighboor-Cells
public int CheckLivingNeighboors(int x, int y)
{
//take the length of X and Y from ActualGeneration.
int Xlength = ActualGeneration.GetLength(0);
int Ylength = ActualGeneration.GetLength(1);

//Start counting from zero neighboors.
int neighbours = 0;

#region CountingNeighboors
//Right
if (x < Xlength - 1)
if (ActualGeneration[x + 1, y].IsAlive)
neighbours++;
//Bottom Right
if (x < Xlength - 1 && y < Ylength - 1)
if (ActualGeneration[x + 1, y + 1].IsAlive)
neighbours++;
//Bottom
if (y < Ylength - 1)
if (ActualGeneration[x, y + 1].IsAlive)
neighbours++;
//Bottom Left
if (x > 0 && y < Ylength - 1)
if (ActualGeneration[x - 1, y + 1].IsAlive)
neighbours++;
//Left
if (x > 0)
if (ActualGeneration[x - 1, y].IsAlive)
neighbours++;
//Top Left
if (x > 0 && y > 0)
if (ActualGeneration[x - 1, y - 1].IsAlive)
neighbours++;
//Top
if (y > 0)
if (ActualGeneration[x, y - 1].IsAlive)
neighbours++;
//Top Right
if (x < Xlength - 1 && y != 0)
if (ActualGeneration[x + 1, y - 1].IsAlive)
neighbours++;
#endregion

return neighbours;
}
}
}

//public void SendToGenTable(double X_index, double Y_index)
//{
//    using (GoLContext db = new GoLContext())
//    {
//        Generation gen = new Generation();
//        var coords = GetActualGeneration();
//        int X = (int)X_index;
//        int Y = (int)Y_index;

//        gen.Cell_X = X;
//        gen.Cell_Y = Y;
//        gen.IsAlive = true;

//        for (int i = 0; i < coords.GetLength(0); i++)
//        {
//            for (int j = 0; j < coords.GetLength(1); j++)
//            {
//                if (coords[i, j].IsAlive == true)
//                {
//                    db.Generation.Add(gen);
//                }
//            }
//        }
//        db.SaveChanges();
//    }
//}



//    --CREATE TABLE Generation
//--(
//--	Gen_id int IDENTITY(1,1) PRIMARY KEY,
//--	GenNumber int NOT NULL,
//--	Cell_X int NOT NULL,
//--	Cell_Y int NOT NULL,
//--	IsAlive bit DEFAULT(0) NOT NULL,
//--	SavedGame_id int FOREIGN KEY REFERENCES SavedGames(SavedGame_id)
//--)

//--CREATE TABLE SavedGames
//--(
//--	SavedGame_id int IDENTITY(1,1) PRIMARY KEY,
//--	GenNumber int NOT NULL,
//--	Player_id int FOREIGN KEY REFERENCES Player(Player_id)
//--)

//--CREATE TABLE Player
//--(
//--	Player_id int IDENTITY(1,1) PRIMARY KEY,
//--	PlayerName varchar(25),
//--	--SavedGame_id int FOREIGN KEY REFERENCES SavedGames(SavedGame_id)
//--)

//--ALTER TABLE Player
//--ADD COLUMN SavedGame_id int

//--ALTER TABLE Player
//--ADD FOREIGN KEY(SavedGame_id) REFERENCES SavedGames(SavedGame_id)
