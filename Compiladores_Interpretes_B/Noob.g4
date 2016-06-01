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



	   /* Expresiones */
expresion : var = variable IGUAL expr	# Asignacion
		   | accesoArray	IGUAL expr	# AsignacionArray
		   | expr						# expre
		   ;

		   /* Operciones Aritmeticas */
expr : SUB expr						# Menos
	 | accesoArray					# getElementoArray
	 | expr op=('*'|'/') expr		# MulDiv
     | expr op=('+'|'-') expr	    # AddSub
     | INT							# int
     | '(' expr ')'					# parens
	 | var = variable				# id
	 | variable '(' parametro ')'	# callFuncion
     ;

	 /* variable */
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
		 | variable '(' parametro ')' ';'					# callProcedimiento
		 | 'return'	ret	';'									# retGeneral
		 | definicion										# sdefincion
		 ;

	ret	 :	expresion										# retFunc
		 |													# retProc
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


accesoArray	:   variable '[' expr ']'				# idAccesoArray
			|   accesoArray '[' expr ']'			# recursionAccesoArray
			;

	/*  Funciones y procedimientos  */

 definicion : 'func' variable '(' ')' '{' sentencia '}'			# definicionFuncion
			| 'proc' variable '(' ')' '{' sentencia '}'			# definicionProcedimiento
			;

	/* Parametros de funciones */
parametro : expresion paramRec		# primerParametro
		  |							# vacioParam
		  ;

paramRec  : ',' expresion paramRec			# otrosParametros
		  |							# parametroVacio
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