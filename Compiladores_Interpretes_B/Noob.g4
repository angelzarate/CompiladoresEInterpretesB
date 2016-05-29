grammar Noob;

@parser::members
{
    protected const int EOF = Eof;
}
 
@lexer::members
{
    protected const int EOF = Eof;
    protected const int HIDDEN = Hidden;
}


/*
 * Parser Rules
 */

prog: sentencia*;

expr : expr op=('*'|'/') expr		# MulDiv
     | expr op=('+'|'-') expr	    # AddSub
     | INT							# int
     | '(' expr ')'					# parens
	 | var = variable				# id
	 | var=variable  '=' expr		#Asig
     ;

variable : ID      # ide
	;

boolean: '!' boolean											# nb
		| boolean '&&' boolean									# band
		| boolean '||' boolean									# bor
		| var = expr rel=('<'|'<='|'>'|'>='|'=='|'!=') expr		# bsencillo
		| '(' boolean ')'										# bparens
		;


sentencia: IF '(' boolean ')' sentencia						# condicion
		 | IF '(' boolean ')' sentencia ELSE sentencia		# condicionElse
		 | WHILE '(' boolean ')' sentencia					# cwhile
		 | DO	sentencia WHILE '(' boolean ')' ';'			# cdowhile
		 | FOR '(' INT ')'									# repetir
		 | expr											
		 ;
/*
 * Lexer Rules
 */
MAY:'<';
MAYIG:'<=';
MEN:'>';
MENIG:'>=';
DIF:'!=';
EQU: '==';

IF: 'if';
ELSE: 'else';
WHILE: 'while';
DO: 'do';
FOR: 'for';


ID:('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')*; 
IGUAL:['='];
INT : [0-9]+;
MUL : '*';
DIV : '/';
ADD : '+';
SUB : '-';
NOT: '!';
AND: '&&';
OR: '||';
WS
    :   (' ' | '\r' | '\n') -> channel(HIDDEN)
    ;