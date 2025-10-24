using Godot;
using System;

public partial class Game : Node
{

	[Signal]
	public delegate void OnDeathEventHandler(int score);

	RichTextLabel scoreContainer;
    public int speed = 150;
    public int distance = 0;
	int score = 0;
	Marker2D spawnpoint;
    Marker2D sawSpawn;
    [Export]
    public PackedScene saw { get; set; }
	[Export]
	public PackedScene tree { get; set; }
	// Called when the node enters the scene tree for the first time.
	[Export]
	public PackedScene heartC { get; set; }
	[Export]
	public PackedScene questionC { get; set; }
    [Export]
    public PackedScene smileC { get; set; }
    public int speed2 = 150;
    public int distance2 = 0;
    RichTextLabel distanceLabel;

    public override void _Ready()
    {
        spawnpoint = GetNode<Marker2D>("treeSpawn");
        sawSpawn = GetNode<Marker2D>("sawSpawn");
        scoreContainer = GetNode<RichTextLabel>("ScoreLayer/Container/Score");
        distanceLabel = GetNode<RichTextLabel>("ScoreLayer/Container/Distance");

    }

    public override void _Process(double delta)
    {
        scoreContainer.Text = score.ToString();
		distance2 += (int)(speed * 0.01);
        distanceLabel.Text = distance2 + " m";

	}



public void OnSawTimeout()
{
    var timer = GetNode<Timer>("sawSpawn/Timer");
    timer.WaitTime = 3;
    Random rand = new Random();

    var sawSpawned = saw.Instantiate<Saw>();
    var position = sawSpawn.Position;
    sawSpawned.Position = position;
    sawSpawned.OnHit += OnDeathAreaBodyEntered;
    AddChild(sawSpawned);
}

bool spawnTreeTurn = true; // Håller koll på vad som ska spawna härnäst
public void OnSpawnTimerTimeout()
{
    Random rand = new Random();

        if (spawnTreeTurn)
        {
            // Vi spawnar alltid en laser först
            var treeSpawn = tree.Instantiate<Tree>();
            int offset = ((int)(rand.Next(1200) / 100) - 6) * 50;
            var position = spawnpoint.Position;
            position.Y += offset;
            treeSpawn.Position = position;
            treeSpawn.OnScore += PlayerHasScored;
            treeSpawn.OnHit += OnDeathAreaBodyEntered;
            AddChild(treeSpawn);
        }
        else
        {
            // Nu spawnar vi HeartC eller QuestionC
            int choice = rand.Next(3); // 0 = Heart, 1 = Question

            if (choice == 0)
            {
                var heartCSpawn = heartC.Instantiate<HeartC>();
                var position_heartC = spawnpoint.Position;
                int offset_C = ((int)(rand.Next(900) / 100) - 6) * 50;
                position_heartC.Y += offset_C;
                heartCSpawn.Position = position_heartC;
                AddChild(heartCSpawn);
            }
            if (choice == 1)
            {
                var questionCSpawn = questionC.Instantiate<QuestionC>();
                var position_questionC = spawnpoint.Position;
                int offset_Q = ((int)(rand.Next(900) / 100) - 6) * 50;
                position_questionC.Y += offset_Q;
                questionCSpawn.Position = position_questionC;
                AddChild(questionCSpawn);
            }
            if (choice == 2)
            {
                var smileCSpawn = smileC.Instantiate<SmileC>();
                var position_smileC = spawnpoint.Position;
                int offset_S = ((int)(rand.Next(900) / 100) - 6) * 50;
                position_smileC.Y += offset_S;
                smileCSpawn.Position = position_smileC;
                AddChild(smileCSpawn);
            }
        }

    spawnTreeTurn = !spawnTreeTurn;
}
	public void PlayerHasScored()
	{
		score++;
		scoreContainer.Text = score.ToString();

	}
		public void OnDeathAreaBodyEntered(Node2D body)
	{
		if (body is Gran)
		{
			EmitSignal(SignalName.OnDeath, score);

			GD.Print("Gran hit an obstacle");
		}
	}
}
