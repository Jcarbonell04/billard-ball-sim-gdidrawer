# GDIDrawer Pool Simulation

A C# pool table simulation project using [GDIDrawer](https://github.com/ISTNAIT/GDIDrawer).  
This program demonstrates physics-based motion for balls, including collisions, friction, and a cue ball aimed with the mouse. Ball stats are displayed in a `DataGridView`, and users can dynamically adjust the number of balls and friction.

## Features / UI
- **Cue Ball**: Aim and shoot using mouse clicks, with velocity proportional to shot vector.
- **Physics**: Realistic collision detection between balls and walls.
- **Friction Control**: Adjust friction in real-time with mouse wheel or label click.
- **Dynamic Ball Count**: Change number of balls using mouse wheel on the "New Table" button.
- **DataGridView Stats**:
  - Ball radius
  - Collisions during the current shot (`#BallsHit %`)
  - Total collisions.

## How to Use
1. Clone my repo.
2. Open the project inn Visual Studio.
3. Run the program.
4. Click the **New Table** button to generate balls.
5. Move your mouse to aim the cue ball.
6. Click to shoot.
7. Observe ball stats in the DataGridView and adjust friction

## File Structure
- `Form1.cs` – main UI and event handling
- `Ball.cs` – ball class, movement, collision, and rendering
- `Table.cs` – table class, ball management, canvas rendering
- `Program.cs` – winForms usage

## Screenshots

