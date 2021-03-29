using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisShapes
{

    public const int ShapeSize = 4;

    public static int[,] IBlock = new int[,] {
                                { 0,0,0,0 },
                                { 0,0,0,0 },
                                { 1,1,1,1 },
                                { 0,0,0,0 }};
    public static int[,] JBlock = new int[,] {
                                { 0,0,0,0 },
                                { 0,1,0,0 },
                                { 0,1,1,1 },
                                { 0,0,0,0 },};

    public static int[,] LBlock = new int[,] {
                                { 0,0,0,0 } ,
                                { 0,0,1,0 } ,
                                { 1,1,1,0 } ,
                                { 0,0,0,0 }};

    public static int[,] OBlock = new int[,] {
                                { 0,0,0,0 } ,
                                { 0,1,1,0 } ,
                                { 0,1,1,0 } ,
                                { 0,0,0,0 },};

    public static int[,] SBlock = new int[,] {
                                { 0,0,0,0 } ,
                                { 0,1,1,0 } ,
                                { 1,1,0,0 } ,
                                { 0,0,0,0 } };

    public static int[,] ZBlock = new int[,] {
                                { 0,0,0,0 } ,
                                { 0,1,1,0 } ,
                                { 0,0,1,1 } ,
                                { 0,0,0,0 } };


    public static int[,] TBlock = new int[,] {
                                { 0,0,0,0 } ,
                                { 0,0,1,0 } ,
                                { 0,1,1,1 } ,
                                { 0,0,0,0 } };

}
