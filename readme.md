---
title: Xu's Test
date: 2019-07-24
---

## Program description in dnaGraph Project(VS 2015 in c#):
### 1. My algorithm introduction
>* I found that the different  merge order of the same length of overlap can cause different result. For example:
> 
>* t e n d s w e l l
>*             e l l t h a t e n
>*                              a l l i s w e l l
> The result is: t e n d s w e l l t h a t e n a l l i s w e l l
>* a l l i s w e l l
>*             e l l t h a t e n
>*                              t e n d s w e l l
> The result is: a l l i s w e l l t h a t e n d s w e l l
>* So I designed an algorithm trying to find the maximum overlap of the whole merging process. That is to find the weightest path. The following is the flow chart of the recursive algorithm.

![image](https://github.com/microxu/DNAmatchdemo/blob/master/images/flowdata.jpg)

>* By the way, consider this kind of completely containing:
>* s1:    s   w e l l   t  h  a
>* s2:    e l l
>* I think KMP algorithm is better than indexof method ,  and it can reduce the time overhead to O(n+m) , especially in the case of an uncertain length of  original string.

### 2. About my programme(dnaTest/dnaTest/Program.cs) ,Please check it: 
>* In this project, to make the program readable, extendable and testable, I designed threes interfaces, and three implement class. one of which includes the main reconstruct logic , DfsReconstructFragment function, of the recursive algorithm, the mian function is in it as well;one is for IO process, the other is the help class, including the KMP search algorithm, the array depth copy method, the overlapping algorithm, and so on

>* It can be run directly, or you can modify the input file (fragments.txt) in the project to run it.

![image](https://github.com/microxu/DNAmatchdemo/blob/master/images/result.jpg)

### 3. About Unit Test(dnaTest/dnaTestTests/ReconstructFragmentsTests.cs)
> Passed the test of some cases.



----------

