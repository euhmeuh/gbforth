title: EXAMPLE

require ibm-font.fs
require term.fs
require cpu.fs
require gbhw.fs

: dot 0 0 at-xy rDIV ? ;

vblank: dot

: nothing ;

: main
  install-font
  init-term
  3 7 at-xy
  ." Hello World !"
  ( cr rIE ? ) cr rIF ? ;
  \ enable-interrupts
   \ 100 0 begin dot again ;
