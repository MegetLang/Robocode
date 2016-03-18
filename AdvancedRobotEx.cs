using System;
using System.Drawing;
using Robocode;

namespace PG4500_2016_Exam1.Robocode
{
    /// <summary>
    /// Extended AdvancedRobot functionality for PG4500 robots.
    /// version: 1.0
    /// author: Tomas Sandnes - santom@westerdals.no
    /// </summary>
    public class AdvancedRobotEx : AdvancedRobot
    {
        // NOTE: The ORDER that Robocode runs is as follows: 
        //		 Firing PITFALL: Gun fires before it turns!
        //       (Source: http://robocode.sourceforge.net/help/physics/physics.html)
        //       1. Battle view is (re)painted. 
        //		 2. All robots Execute their code until they take action (and then paused). 
        //		 3. Time is updated (time = time + 1). 
        //		 4. All bullets move and check for collisions. This includes firing bullets. 
        //		 5. All robots move (gun, radar, heading, acceleration, velocity, distance, in that order). 
        //		 6. All robots perform scans (and collect team messages). 
        //		 7. All robots are resumed to take new action. 
        //		 8. Each robot processes its event queue.


        // P R O P E R T I E S 
        // -------------------

        public EnemyData Enemy { get; set; }  // Stored info about our current radar target. (Wiped each round/match.)


        // P U B L I C   M E T H O D S 
        // ---------------------------

        public AdvancedRobotEx()
        {
            Enemy = new EnemyData();
        }

        public override void OnRobotDeath(RobotDeathEvent deadRobot)
        {
            if (deadRobot.Name == Enemy.Name)
            {
                Enemy.Clear();
            }
        }

        /// <summary>
        /// If one of our bullets just hit our currently tracked target, update the stored data (Energy property) of our target.
        /// </summary>
        public override void OnBulletHit(BulletHitEvent hitData)
        {
            if (Enemy.Name == hitData.VictimName)
            {
                Enemy.Energy = hitData.VictimEnergy;
            }
        }
    }
}
