using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TFModFortRiseLoaderAI;
//using System.Drawing;
//using static TFModFortRiseAiSimple.AISiAgentGraph;

namespace TFModFortRiseAiGraph
{
  public static class AIMovementLibrary
  {
    public static List<MovementAction> BuildLibrary(Agent agent)
    {
      var movementLibrary = new List<MovementAction>();

      ////////////////////////
      var action = new MovementAction("None");
      //action.CalculateCost();
      //action.Condition = (pos, ai) => true;
      //action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X, pos.Y) };
      //movementLibrary.Add(action);
      //////////////////////// ok
      action = new MovementAction("jumpup")
         .AddPhase(new MovementPhase(0.5f, moveY: -1, jump: true)) // ne pas bouger
         //.AddPhase(new MovementPhase(0.0f, moveY: -1, jump: false)) // ne pas bouger
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      agent.IsSolid(pos.X, pos.Y + 1)  // au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                //&& 
                                //!agent.IsSolid(pos.X - 1, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                                new Point(pos.X, pos.Y - 3),
      };
      movementLibrary.Add(action);
      //////////////////////// ok   depart dans l'air possible
      action = new MovementAction("dashup") 
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash:true)) // ne pas bouger
         //.AddPhase(new MovementPhase(0.0f, moveY: -1, jump: false)) // ne pas bouger
        //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)  //pas besoin d etre au sol, peut dashé en l'air
                                      //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                      //&& 
                                      agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& !agent.IsSolid(pos.X, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X, pos.Y - 3) };
      movementLibrary.Add(action);
      //////////////////////// ok   depart dans l'air possible
      action = new MovementAction("dashleft") 
          .AddPhase(new MovementPhase(0.47f, moveX: -1, dash: true)) // ne pas bouger
         //.AddPhase(new MovementPhase(0.0f, moveX: 0, dash: false)) // ne pas bouger
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)  //pas besoin d etre au sol, peut dashé en l'air
                                      //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                      //&& 
                                      agent.IsAreaFree(pos.X, pos.X - 3, pos.Y, pos.Y - 1)  //air libre pour sauté
                                      //&& !agent.IsSolid(pos.X, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 3, pos.Y) };
      movementLibrary.Add(action);
      //////////////////////// ok   depart dans l'air possible
      action = new MovementAction("dashright")
          .AddPhase(new MovementPhase(0.47f, moveX: 1, dash: true)) // ne pas bouger
         //.AddPhase(new MovementPhase(0.0f, moveX: 0, dash: false)) // ne pas bouger
        //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)  //pas besoin d etre au sol, peut dashé en l'air
                                      //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                      //&& 
                                      agent.IsAreaFree(pos.X, pos.X + 3, pos.Y, pos.Y - 1)  //air libre pour sauté
                                                                                            //&& !agent.IsSolid(pos.X, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 3, pos.Y) };
      movementLibrary.Add(action);
      //////////////////////// ok  depart dans l'air possible
      action = new MovementAction("dashleftup")
          .AddPhase(new MovementPhase(0.47f, moveX: -1, moveY: -1, dash: true)) // ne pas bouger
         //.AddPhase(new MovementPhase(0.0f, moveX: 0, moveY: 0, dash: false)) // ne pas bouger
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)  //pas besoin d etre au sol, peut dashé en l'air
                                      //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                      //&& 
                                      agent.IsAreaFree(pos.X, pos.X - 2, pos.Y, pos.Y - 2)  //air libre pour sauté
                                                                                            //&& !agent.IsSolid(pos.X, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 2, pos.Y - 2) };
      movementLibrary.Add(action);
      //////////////////////// ok   depart dans l'air possible
      action = new MovementAction("dashrightup")
          .AddPhase(new MovementPhase(0.47f, moveX: 1, moveY: -1, dash: true)) // ne pas bouger
         //.AddPhase(new MovementPhase(0.0f, moveX: 0, moveY: 0, dash: false)) // ne pas bouger
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)  //pas besoin d etre au sol, peut dashé en l'air
                                      //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                      //&& 
                                      agent.IsAreaFree(pos.X, pos.X + 2, pos.Y, pos.Y - 2)  //air libre pour sauté
                                                                                            //&& !agent.IsSolid(pos.X, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 2, pos.Y - 2) };
      movementLibrary.Add(action);
      //////////////////////// ok
      action = new MovementAction("leftm1")
          .AddPhase(new MovementPhase(1f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.X == startPoint.X - 1)) // ne pas bouger
        //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)
                                      //&& agent.IsSolid(pos.X - 1, pos.Y + 1) 
                                      //&& 
                                      !agent.IsSolid(pos.X - 1, pos.Y)
      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y) };
      movementLibrary.Add(action);
      //////////////////////// ok
      action = new MovementAction("rightp1")
          .AddPhase(new MovementPhase(1f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.X == startPoint.X + 1))  // ne pas bouger
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));
      ;// ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      //agent.IsSolid(pos.X, pos.Y + 1)
                                      //&& agent.IsSolid(pos.X + 1, pos.Y + 1) 
                                      //&& 
                                      !agent.IsSolid(pos.X + 1, pos.Y)
                                      ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y) };
      movementLibrary.Add(action);
      /////////////////////// ok ok
      action = new MovementAction("jumplefetholem2m0")
          .AddPhase(new MovementPhase(0.1f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                  //10000000000000000000000000000001
                                  //10000000000000000000000000000001
                                  //10000000000000000000000000001001
                                  //10000000000000000000000000001101
                                  //11111111111111111111111111111111";
                                  && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
                                  && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 1, pos.Y - 3)  //air libre pour sauté
                                  && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                  //&& agent.IsAreaFree(pos.X, pos.X - 2, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 2, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                      new Point(pos.X - 2, pos.Y), 
                                      new Point(pos.X - 2, pos.Y - 1), 
                                      new Point(pos.X - 2, pos.Y - 2),
      }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok ok
      //action = new MovementAction("jumprightholem2m0")
      //    .AddPhase(new MovementPhase(0.1f, moveX: 1, jump: true))              // continue à avancer en l’air
      //    .AddPhase(new MovementPhase(0.2f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
      //    //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      //;
      //action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
      //                            //10000000000000000000000000000001
      //                            //10000000000000000000000000000001
      //                            //10000000000000000000000000001001
      //                            //10000000000000000000000000001101
      //                            //11111111111111111111111111111111";
      //                            && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
      //                            && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 1, pos.Y - 3)  //air libre pour sauté
      //                            && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
      //                                                                                             //&& agent.IsAreaFree(pos.X, pos.X - 2, pos.Y, pos.Y - 3)  //air libre pour sauté
      //                                //&& agent.IsSolid(pos.X + 2, pos.Y + 1) //sol a l arrivé
      //      ;
      //action.ResultPositions = (pos, ai) => new List<Point> {
      //                                new Point(pos.X + 2, pos.Y),
      //                                new Point(pos.X + 2, pos.Y - 1),
      //                                new Point(pos.X + 2, pos.Y - 2),
      //}; // position finale approximative
      //action.CalculateCost();
      //movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetholem3m0")
          .AddPhase(new MovementPhase(0.15f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000011001
                                      //10000000000000000000000000011001
                                      //11111111111111111111111111110011";
                                      && agent.IsAreaFree(pos.X, pos.X    , pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y - 2, pos.Y - 3)  //air libre pour sauté -1 pour le point de fin y-1
                                      && agent.IsAreaFree(pos.X - 3, pos.X - 3, pos.Y - 2, pos.Y - 3)  //air libre pour sauté -1 pour le point de fin y-1
                                        //&& agent.IsAreaFree(pos.X, pos.X - 3, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 3, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                                    new Point(pos.X - 3, pos.Y),
                                                    new Point(pos.X - 3, pos.Y - 1),
                                                    new Point(pos.X - 3, pos.Y - 2),
              }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightholem3m0")
          .AddPhase(new MovementPhase(0.15f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                        //10000000000000000000000000000001
                                        //10000000000000000000000000000001
                                        //10000000000000000000000000000001
                                        //10000000000000000000000000011001
                                        //10000000000000000000000000011001
                                        //11111111111111111111111111110011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 3)  //air libre pour sauté -1 pour le point de fin y-1
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 2, pos.Y - 3)  //air libre pour sauté -1 pour le point de fin y-1
                                                                                                       //&& agent.IsAreaFree(pos.X, pos.X - 3, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 3, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                    new Point(pos.X + 3, pos.Y),
                                                    new Point(pos.X + 3, pos.Y - 1),
                                                    new Point(pos.X + 3, pos.Y - 2),
              }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetholem4m0")
          .AddPhase(new MovementPhase(0.25f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000011001
                                      //10000000000000000000000000011001
                                      //11111111111111111111111111100011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 3, pos.X - 3, pos.Y - 2, pos.Y - 3)  //air libre pour sauté  -2 pour le point de fin y-1
                                      && agent.IsAreaFree(pos.X - 4, pos.X - 4, pos.Y - 2, pos.Y - 3)  //air libre pour sauté  -2 pour le point de fin y-1
                                //&& agent.IsAreaFree(pos.X, pos.X - 4, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 4, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                                  new Point(pos.X - 4, pos.Y) ,
                                                  new Point(pos.X - 4, pos.Y - 1) ,
                                                  new Point(pos.X - 4, pos.Y - 2) ,
      }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightholem4m0")
          .AddPhase(new MovementPhase(0.25f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000011001
                                      //10000000000000000000000000011001
                                      //11111111111111111111111111100011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 2, pos.Y - 3)  //air libre pour sauté  -2 pour le point de fin y-1
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 2, pos.Y - 3)  //air libre pour sauté  -2 pour le point de fin y-1
                                                                                                       //&& agent.IsAreaFree(pos.X, pos.X - 4, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 4, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                  new Point(pos.X + 4, pos.Y) ,
                                                  new Point(pos.X + 4, pos.Y - 1) ,
                                                  new Point(pos.X + 4, pos.Y - 2) ,
      }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetholem5m0")
          .AddPhase(new MovementPhase(0.37f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000011001
                                      //10000000000000000000000000111001
                                      //11111111111111111111111111000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 3, pos.X - 3, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 4, pos.X - 4, pos.Y-1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 5, pos.X - 5, pos.Y - 1, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                      //&& agent.IsAreaFree(pos.X, pos.X - 5, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 5, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                                              new Point(pos.X - 5, pos.Y) ,
                                                              new Point(pos.X - 5, pos.Y - 1) ,
          }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumrightholem5m0")
          .AddPhase(new MovementPhase(0.37f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.2f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000011001
                                      //10000000000000000000000000111001
                                      //11111111111111111111111111000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y - 1, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                                                                                       //&& agent.IsAreaFree(pos.X, pos.X - 5, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 5, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                              new Point(pos.X + 5, pos.Y) ,
                                                              new Point(pos.X + 5, pos.Y - 1) ,
          }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumplefetholem6m0")
          .AddPhase(new MovementPhase(0.75f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000010001
                                      //10000000000000000000000000111001
                                      //10000000000000000000000010111001
                                      //11111111111111111111111110000011";
                                      && agent.IsAreaFree(pos.X, pos.X      , pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-1, pos.X - 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-2, pos.X - 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-3, pos.X - 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-4, pos.X - 4, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-5, pos.X - 5, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-6, pos.X - 6, pos.Y - 1, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                      //&& agent.IsSolid(pos.X - 6, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                                        new Point(pos.X - 6, pos.Y) ,
                                                        new Point(pos.X - 6, pos.Y - 1) ,
      }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumplrightholem6m0")
          .AddPhase(new MovementPhase(0.75f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000010001
                                      //10000000000000000000000000111001
                                      //10000000000000000000000010111001
                                      //11111111111111111111111110000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 6, pos.X + 6, pos.Y - 1, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                      //&& agent.IsSolid(pos.X + 6, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                        new Point(pos.X + 6, pos.Y) ,
                                                        new Point(pos.X + 6, pos.Y - 1) ,
      }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumplefetholem7m0")
          .AddPhase(new MovementPhase(0.3f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000110001
                                      //10000000000000000000000001111001
                                      //10000000000000000000000101111001
                                      //11111111111111111111111100000011";
                                      && agent.IsAreaFree(pos.X  , pos.X    , pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-1, pos.X - 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-2, pos.X - 2, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-3, pos.X - 3, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-4, pos.X - 4, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-5, pos.X - 5, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-6, pos.X - 6, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-7, pos.X - 7, pos.Y - 1, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                      //&& agent.IsAreaFree(pos.X, pos.X - 7, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 7, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                                          new Point(pos.X - 7, pos.Y), 
                                                          new Point(pos.X - 7, pos.Y-1),
        }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumprightholem7m0")
          .AddPhase(new MovementPhase(0.3f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveX: 1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                                                      //10000000000000000000000000000001
                                                                      //10000000000000000000000000000001
                                                                      //10000000000000000000000000110001
                                                                      //10000000000000000000000001111001
                                                                      //10000000000000000000000101111001
                                                                      //11111111111111111111111100000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 6, pos.X + 6, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 7, pos.X + 7, pos.Y - 1, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                                                                                       //&& agent.IsAreaFree(pos.X, pos.X - 7, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 7, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                          new Point(pos.X + 7, pos.Y),
                                                          new Point(pos.X + 7, pos.Y-1),
        }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumplefetholem7m0doubledash")
          .AddPhase(new MovementPhase(0.3f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.1f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.28f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000110001
                                      //10000000000000000000000101111001
                                      //10000000000000000000000101111001
                                      //11111111111111111111111100000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 3, pos.X - 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 4, pos.X - 4, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 5, pos.X - 5, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 6, pos.X - 6, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 7, pos.X - 7, pos.Y - 2, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                      //&& agent.IsAreaFree(pos.X, pos.X - 7, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 7, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                          new Point(pos.X - 7, pos.Y), 
                                                          new Point(pos.X - 7, pos.Y - 1),
                                                          new Point(pos.X - 7, pos.Y - 2),
        }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);

      action = new MovementAction("jumprightholem7m0doubledash")
          .AddPhase(new MovementPhase(0.3f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.1f, moveX: 1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: 1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.28f, moveX: 1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                                                      //10000000000000000000000000000001
                                                                      //10000000000000000000000000110001
                                                                      //10000000000000000000000101111001
                                                                      //10000000000000000000000101111001
                                                                      //11111111111111111111111100000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 6, pos.X + 6, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 7, pos.X + 7, pos.Y - 2, pos.Y - 4)  //air libre pour sauté -1 pour le point de fin y-1
                                                                                                       //&& agent.IsAreaFree(pos.X, pos.X - 7, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 7, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                          new Point(pos.X + 7, pos.Y),
                                                          new Point(pos.X + 7, pos.Y - 1),
                                                          new Point(pos.X + 7, pos.Y - 2),
        }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumplefetholem8m0dash")
          .AddPhase(new MovementPhase(0.3f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.07f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.24f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000110001
                                      //10000000000000000000000001111001
                                      //10000000000000000000001011111001
                                      //11111111111111111111111000000011";
                                      && agent.IsAreaFree(pos.X  , pos.X    , pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-1, pos.X - 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-2, pos.X - 2, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-3, pos.X - 3, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-4, pos.X - 4, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-5, pos.X - 5, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-6, pos.X - 6, pos.Y-1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-8, pos.X - 7, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-8, pos.X - 8, pos.Y-1, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsAreaFree(pos.X, pos.X - 8, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 8, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { 
                                              new Point(pos.X - 8, pos.Y),
                                              new Point(pos.X - 8, pos.Y-1),
        }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      ///////////////////////  ok
      action = new MovementAction("jumprightholem8m0dash")
          .AddPhase(new MovementPhase(0.3f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.07f, moveX: 1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: 1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.24f, moveX: 1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                                                      //10000000000000000000000000000001
                                                                      //10000000000000000000000000110001
                                                                      //10000000000000000000000001111001
                                                                      //10000000000000000000001011111001
                                                                      //11111111111111111111111000000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 6, pos.X + 6, pos.Y - 1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 8, pos.X + 7, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 8, pos.X + 8, pos.Y - 1, pos.Y - 4)  //air libre pour sauté
                                                                                                       //&& agent.IsAreaFree(pos.X, pos.X - 8, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 8, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                              new Point(pos.X + 8, pos.Y),
                                              new Point(pos.X + 8, pos.Y-1),
        }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetholem9m0dash")
          .AddPhase(new MovementPhase(0.35f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.05f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000001
                                      //10000000000000000000000000000001
                                      //10000000000000000000000001110001
                                      //10000000000000000000000011111001
                                      //10000000000000000000000111111001
                                      //11111111111111111111110000000011";
                                      && agent.IsAreaFree(pos.X  , pos.X    , pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-1, pos.X - 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-2, pos.X - 2, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-3, pos.X - 3, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-4, pos.X - 4, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-5, pos.X - 5, pos.Y-3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-6, pos.X - 6, pos.Y-2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-7, pos.X - 7, pos.Y-1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-8, pos.X - 8, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X-9, pos.X - 9, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsAreaFree(pos.X, pos.X - 9, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 9, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 9, pos.Y) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightholem9m0dash")
          .AddPhase(new MovementPhase(0.35f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.05f, moveX: 1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX: 1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveX:1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0)); ;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                                                      //10000000000000000000000000000001
                                                                      //10000000000000000000000000000001
                                                                      //10000000000000000000000001110001
                                                                      //10000000000000000000000011111001
                                                                      //10000000000000000000000111111001
                                                                      //11111111111111111111110000000011";
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 6, pos.X + 6, pos.Y - 2, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 7, pos.X + 7, pos.Y - 1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 8, pos.X + 8, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 9, pos.X + 9, pos.Y, pos.Y - 4)  //air libre pour sauté
                                                                                                   //&& agent.IsAreaFree(pos.X, pos.X - 9, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 9, pos.Y + 1) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 9, pos.Y) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m1")
          .AddPhase(new MovementPhase(0.05f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 2)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 1, pos.Y - 2)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 1) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m1")
          .AddPhase(new MovementPhase(0.05f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 2)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 1, pos.Y - 2)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y) //sol a l arrivé
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 1) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m2")
          .AddPhase(new MovementPhase(0.08f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 1) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 2) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m2")
          .AddPhase(new MovementPhase(0.08f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 3)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 1) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 2) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok todo mauvaus calcul concidtion au dessus
      action = new MovementAction("jumplefetsurblocm1m3")
          .AddPhase(new MovementPhase(0.27f, moveX: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: -1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 2) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 3) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok todo mauvaus calcul concidtion au dessus
      action = new MovementAction("jumprightsurblocm1m3")
          .AddPhase(new MovementPhase(0.27f, moveX: 1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.5f, moveX: 1, endCondition: (ai, startPoint, startPosition) => ai.playerInfo.onGround))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 3, pos.Y - 4)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 2) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 3) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m4")
          .AddPhase(new MovementPhase(0.1f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.27f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.33f, moveX: -1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 5)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 4, pos.Y - 5)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 3) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 4) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m4")
          .AddPhase(new MovementPhase(0.1f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.27f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.33f, moveX: 1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 5)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 4, pos.Y - 5)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 3) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 4) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m5")
          .AddPhase(new MovementPhase(0.15f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.27f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.30f, moveX: -1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 4) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 5) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m5")
          .AddPhase(new MovementPhase(0.15f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.27f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.30f, moveX: 1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 4) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 5) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m6")
          .AddPhase(new MovementPhase(0.25f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.27f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.30f, moveX: -1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 7)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 6, pos.Y - 7)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 5) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 6) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m6")
          .AddPhase(new MovementPhase(0.25f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.27f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.30f, moveX: 1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 7)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 6, pos.Y - 7)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 5) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 6) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m7")
          .AddPhase(new MovementPhase(0.33f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.30f, moveX: -1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 8)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 7, pos.Y - 8)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 6) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 7) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m7")
          .AddPhase(new MovementPhase(0.33f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.30f, moveX: 1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 8)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 7, pos.Y - 8)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 6) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 7) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumplefetsurblocm1m8")
          .AddPhase(new MovementPhase(0.33f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.40f, moveX: -1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 9)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 8, pos.Y - 9)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 1, pos.Y - 7) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 8) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumprightsurblocm1m8")
          .AddPhase(new MovementPhase(0.33f, moveY: -1, jump: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.40f, moveX: 1, endCondition: null))              // continue à avancer en l’air
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 9)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 8, pos.Y - 9)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 1, pos.Y - 7) //sol a l arrivé
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 8) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// KO a revoir
      //action = new MovementAction("jumplefetsurblocm1m9")
      //    .AddPhase(new MovementPhase(0.33f, moveY: -1, jump: true))              // continue à avancer en l’air
      //    .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
      //    .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
      //    .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))
      //    .AddPhase(new MovementPhase(0.40f, moveX: -1, endCondition: null))              // continue à avancer en l’air
      ////////.AddPhase(new MovementPhase(0.5f, moveX: 0));;  // ne pas bouger
      //;
      //action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
      //                                && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 10)  //air libre pour sauté
      //                                && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 9, pos.Y - 10)  //air libre pour sauté
      //                                && agent.IsSolid(pos.X - 1, pos.Y - 8) //sol a l arrivé
      //          ;
      //action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 9) }; // position finale approximative
      //action.CalculateCost();
      //movementLibrary.Add(action);
      ////////////////////////
      action = new MovementAction("ledgegrableftout")
          .AddPhase(new MovementPhase(0.5f, jump: true, moveX: -1));  // ne pas bouger
      action.CalculateCost();
      action.Condition = (pos, ai) => agent.IsSolid(pos.X - 1, pos.Y) 
                                      && !agent.IsSolid(pos.X - 1, pos.Y - 1) //mur a gauche et vide dessous, possible grab
            ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 1, pos.Y - 1) };
      movementLibrary.Add(action);
      ////////////////////////
      action = new MovementAction("ledgegrabrightout")
          .AddPhase(new MovementPhase(0.5f, jump: true, moveX: 1));  // ne pas bouger
      action.CalculateCost();
      action.Condition = (pos, ai) => agent.IsSolid(pos.X + 1, pos.Y)
                                    && !agent.IsSolid(pos.X + 1, pos.Y - 1) //mur a gauche et vide dessous, possible grab
                                    ; 
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 1, pos.Y - 1) };
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumpupleftm9m5")
          .AddPhase(new MovementPhase(0.35f, moveX: -1, moveY:-1, jump: true))  // ne pas bouger
          .AddPhase(new MovementPhase(0.001f, moveX: -1, moveY: -1, dash: true))  // ne pas bouger
          .AddPhase(new MovementPhase(0.001f, moveX: -1, moveY: -1))  // ne pas bouger
          .AddPhase(new MovementPhase(0.3f, moveX: -1, moveY: -1, dash: true))  // ne pas bouger
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                      //10000000000000000000000000000111
                                      //10000000000000000000011111000001
                                      //10000000000000000000011111100001
                                      //10000000000000000000011111110001
                                      //10000000000000000000011111111001
                                      //10000000000000000000011111111101
                                      //11111111111111111111111111111111";
                                      && agent.IsAreaFree(pos.X - 0, pos.X - 0, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y - 2, pos.Y - 5)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 3, pos.X - 3, pos.Y - 3, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 4, pos.X - 4, pos.Y - 4, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 5, pos.X - 5, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 6, pos.X - 6, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 7, pos.X - 7, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 8, pos.X - 8, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 9, pos.X - 9, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y - 8, pos.Y - 9)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X - 9, pos.Y - 4) //sol a l arrivé  //SOL
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 9, pos.Y - 5) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumpuprightm9m5")
          .AddPhase(new MovementPhase(0.35f, moveX: 1, moveY: -1, jump: true))  // ne pas bouger
          .AddPhase(new MovementPhase(0.001f, moveX: 1, moveY: -1, dash: true))  // ne pas bouger
          .AddPhase(new MovementPhase(0.001f, moveX: 1, moveY: -1))  // ne pas bouger
          .AddPhase(new MovementPhase(0.3f, moveX: 1, moveY: -1, dash: true))  // ne pas bouger
          //.AddPhase(new MovementPhase(0.5f, moveX: 0));  // ne pas bouger
      ;
      action.Condition = (pos, ai) => agent.IsSolid(pos.X, pos.Y + 1) // ne peut sauter que si au sol
                                                                      //10000000000000000000000000000111
                                                                      //10000000000000000000011111000001
                                                                      //10000000000000000000011111100001
                                                                      //10000000000000000000011111110001
                                                                      //10000000000000000000011111111001
                                                                      //10000000000000000000011111111101
                                                                      //11111111111111111111111111111111";
                                      && agent.IsAreaFree(pos.X + 0, pos.X + 0, pos.Y, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 1, pos.Y - 4)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y - 2, pos.Y - 5)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 3, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 4, pos.X + 4, pos.Y - 4, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 5, pos.X + 5, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 6, pos.X + 6, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 7, pos.X + 7, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 8, pos.X + 8, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 9, pos.X + 9, pos.Y - 5, pos.Y - 6)  //air libre pour sauté
                                      && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y - 8, pos.Y - 9)  //air libre pour sauté
                                      //&& agent.IsSolid(pos.X + 9, pos.Y - 4) //sol a l arrivé  //SOL
                ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 9, pos.Y - 5) }; // position finale approximative
      action.CalculateCost();
      movementLibrary.Add(action);
      //////////////////////// ok pas besoin d etre au sol au depart
      action = new MovementAction("walljumpleftm3")
          .AddPhase(new MovementPhase(0.4f, jump: true, moveX: 0))  // ne pas bouger
                                                                  //.AddPhase(new MovementPhase(0f, jump: false, moveX: 0));  // ne pas bouger
      ;
      action.CalculateCost();
      action.Condition = (pos, ai) => !agent.IsSolid(pos.X, pos.Y + 1)//pas sur sol
                                    && agent.IsSolid(pos.X + 1, pos.Y) //mur a droite
                                    && agent.IsAreaFree(pos.X    , pos.X,     pos.Y, pos.Y - 2)  //air libre pour sauté
                                    && agent.IsAreaFree(pos.X - 1, pos.X - 1, pos.Y, pos.Y - 3)  //air libre pour sauté
                                    && agent.IsAreaFree(pos.X - 2, pos.X - 2, pos.Y, pos.Y - 3)  //air libre pour sauté
                                    && agent.IsAreaFree(pos.X - 3, pos.X - 3, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                    ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X - 3, pos.Y - 2) };
      movementLibrary.Add(action);
      //////////////////////// ok  pas besoin d etre au sol au depart
      action = new MovementAction("walljumprightp3")
          .AddPhase(new MovementPhase(0.4f, jump: true, moveX: 0))  // ne pas bouger
                                                                    //.AddPhase(new MovementPhase(0f, jump: false, moveX: 0));  // ne pas bouger
      ;
      action.CalculateCost();
      action.Condition = (pos, ai) => !agent.IsSolid(pos.X, pos.Y + 1)//pas sur sol
                                    && agent.IsSolid(pos.X - 1, pos.Y) //mur a gauche
                                    && agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 2)  //air libre pour sauté
                                    && agent.IsAreaFree(pos.X + 1, pos.X + 1, pos.Y, pos.Y - 3)  //air libre pour sauté
                                    && agent.IsAreaFree(pos.X + 2, pos.X + 2, pos.Y, pos.Y - 3)  //air libre pour sauté
                                    && agent.IsAreaFree(pos.X + 3, pos.X + 3, pos.Y - 2, pos.Y - 3)  //air libre pour sauté
                                    ;
      action.ResultPositions = (pos, ai) => new List<Point> { new Point(pos.X + 3, pos.Y - 2) };
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumpup11") //todo mettre un poids plus fort pour en pas l'utilsier tout le temps
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))

          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))

          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 11)  //air libre pour sauté
      ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                new Point(pos.X, pos.Y - 11),
      };
      movementLibrary.Add(action);
      /////////////////////// ok
      action = new MovementAction("jumpup14") //todo mettre un poids plus fort pour en pas l'utilsier tout le temps
          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))       

          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))

          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))

          .AddPhase(new MovementPhase(0.001f, moveY: -1, dash: true))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.001f, moveY: -1))              // continue à avancer en l’air
          .AddPhase(new MovementPhase(0.4f, moveY: -1, dash: true))
          .AddPhase(new MovementPhase(0.02f, moveY: -1))
      ;  // ne pas bouger
      action.CalculateCost();
      //action.Condition = (pos, ai) => true; // on peux avancer dans le vide et depart dans le vide
      action.Condition = (pos, ai) =>
                                      agent.IsAreaFree(pos.X, pos.X, pos.Y, pos.Y - 15)  //air libre pour sauté
      ;
      action.ResultPositions = (pos, ai) => new List<Point> {
                                                new Point(pos.X, pos.Y - 14),
                                                new Point(pos.X, pos.Y - 13),
      };
      movementLibrary.Add(action);
      /////////////////////// ok
      return movementLibrary;
    }
  }
}
