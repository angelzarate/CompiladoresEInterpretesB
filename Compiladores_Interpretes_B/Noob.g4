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

prog: sentencia fin # programa
	;

fin :			# Final
	;

bloque : bloque sentencia		# bloqRec
	   | sentencia				# bloqSent
	   ;




expr : SUB expr						# Menos
	 | expr op=('*'|'/') expr		# MulDiv
     | expr op=('+'|'-') expr	    # AddSub
     | INT							# int
     | '(' expr ')'					# parens
	 | var = variable				# id
     ;

 expresion : var = variable IGUAL expr	#Asignacion
		   | expr						#expre
		   ;

variable : ID      # ide
	;

	/* bloques booleanos */
boolean: '!' boolean											# nb
		| boolean '&&' boolean									# band
		| boolean '||' boolean									# bor
		| var = expr rel=('<'|'<='|'>'|'>='|'=='|'!=') expr		# bsencillo
		| '(' boolean ')'										# bparens
		;

		/* sentencias del programa */
sentencia: IF '(' boolean ')' sentencia sentelse			# condicion
		 | WHILE '(' boolean ')' sentencia					# swhile
		 | DO	sentencia WHILE '(' boolean ')' ';'			# sdowhile
		 | REPETIR '(' INT ')'								# srepetir
		 | expresion ';'									# senExpr
		 | '{' bloque  '}'									# sentBloque
		 | declaracion										# sdeclaracion
		 ;

sentelse: ELSE sentencia			# celse
		|							# vacio
		;

/* Declaracion de variables */
declaracion	: tipo variable array otroId								# inicioDeclaracion
			;


	tipo	:	type = ('int' | 'float' | 'char')				# declaracionTipo
			;

	array	:	'[' n = INT ']'		array							# declaracionArray
			|													# declaracionArrayVacio
			;

	otroId	:	',' variable	array otroId							# declaracionOtroId
			|	';'												# finDeclaracion
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
REPETIR: 'repetir';

Int: 'int';
Float: 'float';
Char: 'char';


ID:('a'..'z'|'A'..'Z') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')*; 
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