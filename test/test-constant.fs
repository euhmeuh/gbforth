[asm]

$42 constant foo
$9001 constant bar

: test
  foo bar ;

main:
' test # call,

begin, halt, repeat,

[endasm]