title: SPRITE-DEMO
makercode: TK

require gbhw.fs
require input.fs
require lcd.fs
require ibm-font.fs

( program start )

_OAM 0 + constant player.y
_OAM 1 + constant player.x
_OAM 2 + constant player.tile
_OAM 3 + constant player.opts

: playerX+! player.x lcd-wait-vblank +! ;
: playerY+! player.y lcd-wait-vblank +! ;

: handle-input
  begin
    key-state
    dup k-right and if  1 playerX+! then
    dup k-left  and if -1 playerX+! then
    dup k-up    and if -1 playerY+! then
    dup k-down  and if  1 playerY+! then
    dup k-a     and if  1 player.tile lcd-wait-vblank c! then
    dup k-b     and if  2 player.tile lcd-wait-vblank c! then
    \ If there no key pressed, wait for one
    0= if halt then
  again ;

: enable-sprites
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BGOFF or
    LCDCF_OBJ8 or
    LCDCF_OBJON or ]L
  rLCDC c! ;

: clear-background
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L blank ;

: clear-sprites
  _OAM [ 40 4 * ]L erase ;

: init-player-sprite
  #1        player.tile c!
  %00000000 player.opts c!
  #80       player.y    c!
  #50       player.x    c! ;

: main
  init-input
  install-font
  disable-lcd
  clear-background
  clear-sprites
  init-player-sprite
  enable-sprites
  handle-input ;
