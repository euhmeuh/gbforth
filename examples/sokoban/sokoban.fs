require term.fs
require ibm-font.fs
require input.fs

: key? key-state 0<> ;

title: GOLIFE

\ The fast wrapping requires dimensions that are powers of 2.
1 4 lshift constant w \ 16
1 4 lshift constant h \ 16

:m rows    w * 2* ;
1 rows constant row
h rows constant size

ram
create world size allot
( world )  value old
( old w + ) value new

variable gens
: clear  world size erase     0 gens ! ;
: age  new old to new to old  1 gens +! ;

: col+  1+ ;
: col-  1- dup w and + ; \ avoid borrow into row
: row+  row + ;
: row-  row - ;
: wrap ( i -- i ) [ size w - 1- ] literal and ;
: w@ ( i -- 0/1 ) wrap old + c@ ;
: w! ( 0/1 i -- ) wrap old + c! ;

: foreachrow ( xt -- )
  size 0 do  I over execute  row +loop drop ;

: showrow ( i -- ) cr
  old + w over + swap do I c@ if [char] * else bl then emit loop ;
: show  ['] showrow foreachrow  cr ." Generation " gens @ . ;

: sum-neighbors ( i -- i n )
  dup  col- row- w@
  over      row- w@ +
  over col+ row- w@ +
  over col-      w@ +
  over col+      w@ +
  over col- row+ w@ +
  over      row+ w@ +
  over col+ row+ w@ + ;
: gencell ( i -- )
  sum-neighbors  over old + c@
  or 3 = 1 and   swap new + c! ;
: genrow ( i -- )
  w over + swap do I gencell loop ;
: gen  ['] genrow foreachrow  age ;

: life  begin gen 0 0 at-xy show key? until ;

\ patterns
: pat ( i addr len -- )
  rot dup 2swap  over + swap do
    I c@ [char] | = if drop row+ dup else
    I c@ bl  = 1+ over w!  col+ then
  loop 2drop ;

: blinker s" ***" pat ;
: toad s" ***| ***" pat ;
: pentomino s" **| **| *" pat ;
: pi s" **| **|**" pat ;
: glider s"  *|  *|***" pat ;
: pulsar s" *****|*   *" pat ;
: ship s"  ****|*   *|    *|   *" pat ;
: pentadecathalon s" **********" pat ;
: clock s"  *|  **|**|  *" pat ;


: main
  install-font
  init-term
  init-input
  page
  0 0 at-xy


  world to old
  old w + to new

  clear
  2 glider life

\  ." game of life"
;

(
clear  0 glider show
 *
  *
***

Generation 0  ok
gen show

* *
 **
 *
Generation 1  ok
)
