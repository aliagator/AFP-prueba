var SV_MENU = "2.9.4.0";

/* Seccion de definiciones de variables para el aspecto / comportamiento del menu */

//var FontHighColor='yellow';
//var BorderBtwnMain=1;
//var BorderBtwnSub=1;
//var FontBold=0;
//var FontItalic=0;
//var MenuTextCentered='left';
//var MenuCentered='center';
//var MenuVerticalCentered='top';
//var ChildOverlap=0;
//var ChildVerticalOverlap=0;
//var StartTop=0;
var CurrentStartTop=0; /* debe tener el mismo valor que la anterior */
//var StartLeft=0;
//var VerCorrect=17;
//var HorCorrect=0;
//var LeftPadding=0;
//var TopPadding=2;
//var FirstLineHorizontal=0;
//var MenuFramesVertical=1;
//var DissapearDelay=1000;
//var UnfoldDelay=50;
//var TakeOverBgColor=0;
//var FirstLineFrame='Sidebar';
//var SecLineFrame='Body';
//var DocTargetFrame='Body';
//var TargetLoc='';
//var MenuWrap=1;
//var RightToLeft=0;
//var BottomUp=0;
//var UnfoldsOnClick=0;
//var BaseHref='';
//var MenuUsesFrames=1;
//var RememberStatus=0;
//var PartOfWindow=0;
//var MenuSlide='progid:DXImageTransform.Microsoft.GradientWipe(duration=.2, wipeStyle=1)';
//var MenuShadow='progid:DXImageTransform.Microsoft.Shadow(color=#888888, direction=135, strength=3)';
//var MenuOpacity='';

/* No pasar */
var Arrws=['IMG/MENU/tri.gif',5,10,'IMG/MENU/tridown.gif',10,5,'IMG/MENU/trileft.gif',5,10,'IMG/MENU/triup.gif',10,5];
var statusText='';

function BeforeStart(){return}
function AfterBuild(){return}
function BeforeFirstOpen(){return}
function AfterCloseAll(){return}

/*(c) Ger Versluis 2000 version 9.10  16 November 2002
You may use this script on non commercial sites. 
For info write to menus@burmees.nl*/
var AgntUsr=navigator.userAgent.toLowerCase(),AppVer=navigator.appVersion.toLowerCase();
var DomYes=document.getElementById?1:0,NavYes=AgntUsr.indexOf("mozilla")!=-1&&AgntUsr.indexOf("compatible")==-1?1:0,ExpYes=AgntUsr.indexOf("msie")!=-1?1:0,Opr=AgntUsr.indexOf("opera")!=-1?1:0;
var DomNav=DomYes&&NavYes?1:0,DomExp=DomYes&&ExpYes?1:0;
var Nav4=NavYes&&!DomYes&&document.layers?1:0,Exp4=ExpYes&&!DomYes&&document.all?1:0;
var MacCom=(AppVer.indexOf("mac")!= -1)?1:0,MacExp4=(MacCom&&AppVer.indexOf("msie 4")!= -1)?1:0,Mac4=(MacCom&&(Nav4||Exp4))?1:0;
var Exp5=AppVer.indexOf("msie 5")!= -1?1:0,Fltr=(AppVer.indexOf("msie 6")!= -1||AppVer.indexOf("msie 7")!= -1)?1:0,MacExp5=(MacCom&&Exp5)?1:0,PosStrt=(NavYes||ExpYes)&&!Opr?1:0;

var Ztop=100,InitLdd=0,P_X=DomYes?"px":"";
//var OpnTmr=null;
var arregloMenus;
var MenusID;
var currentMenu;
var waitForGo;

/*
if(PosStrt){
	if(MacExp4||MacExp5)LdTmr=setTimeout("ChckInitLd()",100);
	else{
		if(currentMenu.Trigger.onload)Dummy=currentMenu.Trigger.onload;
		if(DomNav)currentMenu.Trigger.addEventListener("load",Go(currentMenu),false);
		else{
			currentMenu.Trigger.onload=Go
		}
	}
}
*/

function ChckInitLd(ctl){
	InitLdd=(ctl.MenuUsesFrames)?(ctl.Par.document.readyState=="complete"&&ctl.Par.frames[ctl.FirstLineFrame].document.readyState=="complete"&&ctl.Par.frames[ctl.SecLineFrame].document.readyState=="complete")?1:0:(ctl.Par.document.readyState=="complete")?1:0;
	if(InitLdd){clearInterval(LdTmr);Go(ctl)}}

function Dummy(){return}

function CnclSlct(){return false}

function RePos(ctl){

	for(i=0;i<arregloMenus.length;i++) {
	  try {
		ctl=arregloMenus[i];
		ctl.FWinW=ExpYes?ctl.FLoc.document.body.clientWidth:ctl.FLoc.innerWidth;
		ctl.FWinH=ExpYes?ctl.FLoc.document.body.clientHeight:ctl.FLoc.innerHeight;
		ctl.SWinW=ExpYes?ctl.ScLoc.document.body.clientWidth:ctl.ScLoc.innerWidth;
		ctl.SWinH=ExpYes?ctl.ScLoc.document.body.clientHeight:ctl.ScLoc.innerHeight;
		if(ctl.MenuCentered.indexOf("justify")!=-1&&ctl.FirstLineHorizontal){
			ClcJus(ctl);
			var P=ctl.FrstCntnr.FrstMbr,W=Menu1[5],a=ctl.BorderBtwnMain?ctl.NoOffFirstLineMenus+1:2,i;
			ctl.FrstCntnr.style.width=ctl.NoOffFirstLineMenus*W+a*ctl.BorderWidthMain+P_X;
			for(i=0;i<ctl.NoOffFirstLineMenus;i++){
				P.style.width=W-(P.value.indexOf("<")==-1?ctl.LftXtra:0)+P_X;
				if(P.ai&&!ctl.RightToLeft)P.ai.style.left=ctl.BottomUp?W-Arrws[10]-2+P_X:W-Arrws[4]-2+P_X;
				P=P.PrvMbr}}
		ctl.StaticPos=-1;
		ClcRl(ctl);
		if(ctl.TargetLoc)ClcTrgt(ctl);ClcLft(ctl);ClcTp(ctl);
		
		PosMenu(ctl,ctl.FrstCntnr,ctl.StartTop,ctl.StartLeft);
		if(ctl.RememberStatus)StMnu(ctl);
		scroll_RePos(ctl);
	  } catch (e) {};
	}
	
}

function NavUnLdd(ctl){
	if (typeof(ctl) == "undefined") {
		ctl=currentMenu;
	}
	ctl.Ldd=0;ctl.Crtd=0;SetMenu="0";
}

function UnLdd(ctl){
	if (typeof(ctl) == "undefined") {
		ctl=currentMenu;
	}
	ctlID = ctl.id;
	NavUnLdd(ctl);
	if(ExpYes){var M=ctl.FrstCntnr?ctl.FrstCntnr.FrstMbr:null;
		while(M!=null){if(M.CCn){MakeNull(M.CCn);M.CCn=null}
			M=M.PrvMbr}}
	if(!Nav4){LdTmr=setTimeout("ChckLddByID('" + ctlID + "')",100)}}

function UnLddTotal(ctl){
	if (typeof(ctl) == "undefined") {
		ctl=currentMenu;
	}
	MakeNull(ctl.FrstCntnr);ctl.FrstCntnr=ctl.RmbrNow=ctl.FLoc=ctl.ScLoc=ctl.DcLoc=ctl.SLdAgnWin=ctl.CurOvr=ctl.CloseTmr=Doc=Bod=Trigger=null
}

function MakeNull(P){
	var M=P.FrstMbr,Mi;
	while(M!=null){Mi=M;
		if(M.CCn){MakeNull(M.CCn);M.CCn=null}
		M.Cntnr=null;M=M.PrvMbr;Mi.PrvMbr=null;Mi=null}
	P.FrstMbr=null}

function ChckLddByID(ctlID){
	ChckLdd(document.all[ctlID]);
}

function ChckLdd(ctl){
  try {
	if(!ExpYes){if(ctl.ScLoc.document.body){clearInterval(LdTmr);Go(ctl)}}
	else if(ctl.ScLoc.document.readyState=="complete"){if(LdTmr)clearInterval(LdTmr);Go(ctl)}
  } catch (e) {}	
}
	
function NavLdd(ctl,e){
	if (typeof(ctl) == "undefined") {
		ctl=currentMenu;
	}
	if(e.target!=self)routeEvent(e);if(e.target==ctl.ScLoc)Go(ctl)
}

function ReDoWhole(ctl){
	if (typeof(ctl) == "undefined") {
		ctl=currentMenu;
	}
	if(AppVer.indexOf("4.0")==-1)Doc.location.reload();else if(ctl.SWinW!=ctl.ScLoc.innerWidth||ctl.SWinH!=ctl.ScLoc.innerHeight||ctl.FWinW!=ctl.FLoc.innerWidth||ctl.FWinH!=ctl.FLoc.innerHeight)Doc.location.reload()
}


function Go(ctl){

	currentMenu = ctl;
	
	if(!ctl.inicVars) {
		//definicion de variables iniciales
		inicializaVars(ctl);
		ctl.inicVars=1;
	}
	
	if(!ctl.Ldd&&PosStrt){
		BeforeStart();
		ctlID=ctl.id;
		ctl.Crtd=0;ctl.Ldd=1;
		ctl.FLoc=ctl.MenuUsesFrames?parent.frames[ctl.FirstLineFrame]:window;
		ctl.ScLoc=ctl.MenuUsesFrames?parent.frames[ctl.SecLineFrame]:window;
		ctl.DcLoc=ctl.MenuUsesFrames?parent.frames[ctl.DocTargetFrame]:window;
		
		if(ctl.MenuUsesFrames){
			if(!ctl.FLoc){ctl.FLoc=ctl.ScLoc;if(!ctl.FLoc){ctl.FLoc=ctl.ScLoc=ctl.DcLoc;if(!ctl.FLoc)ctl.FLoc=ctl.ScLoc=ctl.DcLoc=window}}
			if(!ctl.ScLoc){ctl.ScLoc=ctl.DcLoc;if(!ctl.ScLoc)ctl.ScLoc=ctl.DcLoc=ctl.FLoc}
			if(!ctl.DcLoc)ctl.DcLoc=ctl.ScLoc}
		if(ctl.FLoc==ctl.ScLoc)ctl.AcrssFrms=0;
		if(ctl.AcrssFrms)ctl.FirstLineHorizontal=ctl.MenuFramesVertical?0:1;
		ctl.FWinW=ExpYes?ctl.FLoc.document.body.clientWidth:ctl.FLoc.innerWidth;
		ctl.FWinH=ExpYes?ctl.FLoc.document.body.clientHeight:ctl.FLoc.innerHeight;
		ctl.SWinW=ExpYes?ctl.ScLoc.document.body.clientWidth:ctl.ScLoc.innerWidth;
		ctl.SWinH=ExpYes?ctl.ScLoc.document.body.clientHeight:ctl.ScLoc.innerHeight;
		ctl.FColW=Nav4?ctl.FLoc.document:ctl.FLoc.document.body;
		ctl.SColW=Nav4?ctl.ScLoc.document:ctl.ScLoc.document.body;
		ctl.DColW=Nav4?ctl.DcLoc.document:ctl.ScLoc.document.body;
		if(ctl.TakeOverBgColor){
			if(ExpYes&&MacCom)ctl.FColW.style.backgroundColor=ctl.AcrssFrms?ctl.SColW.bgColor:ctl.DColW.bgColor;
			else ctl.FColW.bgColor=ctl.AcrssFrms?ctl.SColW.bgColor:ctl.DColW.bgColor}
		if(ctl.MenuCentered.indexOf("justify")!=-1&&ctl.FirstLineHorizontal)ClcJus(ctl);
		
		if(ctl.FrstCreat||ctl.FLoc==ctl.ScLoc)ctl.FrstCntnr=CreateMenuStructure(ctl,"Menu",ctl.NoOffFirstLineMenus,null);
		else CreateMenuStructureAgain(ctl,"Menu",ctl.NoOffFirstLineMenus);
			ClcRl(ctl);
		if(ctl.TargetLoc)ClcTrgt(ctl);ClcLft(ctl);ClcTp(ctl);
		PosMenu(ctl,ctl.FrstCntnr,ctl.StartTop,ctl.StartLeft);
		ctl.IniFlg=1;Initiate(ctl);ctl.Crtd=1;
		
		ctl.SLdAgnWin=ExpYes?ctl.ScLoc.document.body:ctl.ScLoc;ctl.SLdAgnWin.onunload=Nav4?NavUnLdd:UnLdd;
		
		if(ExpYes)ctl.Trigger.onunload=UnLddTotal;
		ctl.Trigger.onresize=Nav4?ReDoWhole:RePos;
		AfterBuild();
		if(ctl.RememberStatus)StMnu(ctl);
		if(Nav4&&ctl.FrstCreat){ctl.Trigger.captureEvents(Event.LOAD);ctl.Trigger.onload=NavLdd}
		if(ctl.FrstCreat){Dummy();ctl.FrstCreat=0;}
		if(ctl.MenuVerticalCentered=="static"&&!ctl.AcrssFrms)setTimeout("KeepPos('" +  ctlID + "')",250)	
	}
}
	
function KeepPos(ctlID){
	ctlId = "table1";
	var ctl = document.all[ctlID];
	var TS=ExpYes?ctl.FLoc.document.body.scrollTop:ctl.FLoc.pageYOffset;
	if(TS!=ctl.StaticPos){var FCSt=Nav4?ctl.FrstCntnr:ctl.FrstCntnr.style;
		ctl.FrstCntnr.OrgTop=ctl.StartTop+TS;FCSt.top=ctl.FrstCntnr.OrgTop+P_X;ctl.StaticPos=TS}}

function ClcRl(ctl){
	ctl.StartTop=ctl.M_StrtTp<1&&ctl.M_StrtTp>0?ctl.M_StrtTp*ctl.FWinH:ctl.M_StrtTp;
	ctl.StartLeft=ctl.M_StrtLft<1&&ctl.M_StrtLft>0?ctl.M_StrtLft*ctl.FWinW:ctl.M_StrtLft}

function ClcJus(ctl){
	var a=ctl.BorderBtwnMain?ctl.NoOffFirstLineMenus+1:2,Sz=Math.round((ctl.PartOfWindow*ctl.FWinW-a*ctl.BorderWidthMain)/ctl.NoOffFirstLineMenus),i,j;
	for(i=1;i<ctl.NoOffFirstLineMenus+1;i++){j=eval(ctl.id+i);j[5]=Sz}
	ctl.StartLeft=0}

function ClcTrgt(ctl){
	var TLoc=Nav4?ctl.FLoc.document.layers[ctl.TargetLoc]:DomYes?ctl.FLoc.document.getElementById(ctl.TargetLoc):ctl.FLoc.document.all[ctl.TargetLoc];
	if(DomYes){while(TLoc){ctl.StartTop+=TLoc.offsetTop;ctl.StartLeft+=TLoc.offsetLeft;TLoc=TLoc.offsetParent}}
	else{ctl.StartTop+=Nav4?TLoc.pageY:TLoc.offsetTop;ctl.StartLeft+=Nav4?TLoc.pageX:TLoc.offsetLeft}}

function ClcLft(ctl){
	if(ctl.MenuCentered.indexOf("left")==-1){
		var Sz=ctl.FWinW-(!Nav4?parseInt(ctl.FrstCntnr.style.width):ctl.FrstCntnr.clip.width);
		ctl.StartLeft+=ctl.MenuCentered.indexOf("right")!=-1?Sz:Sz/2;
		if(ctl.StartLeft<0)ctl.StartLeft=0}}

function ClcTp(ctl){
	if(ctl.MenuVerticalCentered!="top"&&ctl.MenuVerticalCentered!="static"){
		var Sz=ctl.FWinH-(!Nav4?parseInt(ctl.FrstCntnr.style.height):ctl.FrstCntnr.clip.height);
		ctl.StartTop+=ctl.MenuVerticalCentered=="bottom"?Sz:Sz/2;
		if(ctl.StartTop<0)ctl.StartTop=0}}

function PosMenu(ctl,Ct,Tp,Lt){
	ctl.RLvl++;
	var Ti,Li,Hi,Mb=Ct.FrstMbr,CStl=!Nav4?Ct.style:Ct,MStl=!Nav4?Mb.style:Mb,PadL=Mb.value.indexOf("<")==-1?ctl.LftXtra:0,PadT=Mb.value.indexOf("<")==-1?ctl.TpXtra:0,MWt=!Nav4?parseInt(MStl.width)+PadL:MStl.clip.width,MHt=!Nav4?parseInt(MStl.height)+PadT:MStl.clip.height,CWt=!Nav4?parseInt(CStl.width):CStl.clip.width,CHt=!Nav4?parseInt(CStl.height):CStl.clip.height,CCw,CCh,STp,SLt;
	var BRW=ctl.RLvl==1?ctl.BorderWidthMain:ctl.BorderWidthSub,BTWn=ctl.RLvl==1?ctl.BorderBtwnMain:ctl.BorderBtwnSub;
	if(ctl.RLvl==1&&ctl.AcrssFrms)!ctl.MenuFramesVertical?Tp=ctl.BottomUp?0:ctl.FWinH-CHt+(Nav4?MacCom?-2:4:0):Lt=ctl.RightToLeft?0:ctl.FWinW-CWt+(Nav4?MacCom?-2:4:0);
	if(ctl.RLvl==2&&ctl.AcrssFrms)!ctl.MenuFramesVertical?Tp=ctl.BottomUp?ctl.SWinH-CHt+(Nav4?MacCom?-2:4:0):0:Lt=ctl.RightToLeft?ctl.SWinW-CWt:0;
	if(ctl.RLvl==2){Tp+=ctl.VerCorrect;Lt+=ctl.HorCorrect}
	
	CStl.top=ctl.RLvl==1?Tp+P_X:0;Ct.OrgTop=Tp;
	CStl.left=ctl.RLvl==1?Lt+P_X:0;Ct.OrgLeft=Lt;
	if(ctl.RLvl==1&&ctl.FirstLineHorizontal){Hi=1;Li=CWt-MWt-2*BRW;Ti=0}
	else{Hi=Li=0;Ti=CHt-MHt-2*BRW}
	while(Mb!=null){
		MStl.left=Li+BRW+P_X;
		MStl.top=Ti+BRW+P_X;
		if(Nav4)Mb.CLyr.moveTo(Li+BRW,Ti+BRW);
		if(Mb.CCn){if(ctl.RightToLeft)CCw=Nav4?Mb.CCn.clip.width:parseInt(Mb.CCn.style.width);
			if(ctl.BottomUp)CCh=Nav4?Mb.CCn.clip.height:parseInt(Mb.CCn.style.height);
			if(Hi){STp=ctl.BottomUp?Ti-CCh:Ti+MHt+2*BRW;SLt=ctl.RightToLeft?Li+MWt-CCw:Li}
			else{SLt=ctl.RightToLeft?Li-CCw+ctl.ChildOverlap*MWt+BRW:Li+(1-ctl.ChildOverlap)*MWt;
				STp=ctl.RLvl==1&&ctl.AcrssFrms?ctl.BottomUp?Ti-CCh+MHt:Ti:ctl.BottomUp?Ti-CCh+(1-ctl.ChildVerticalOverlap)*MHt+2*BRW:Ti+ctl.ChildVerticalOverlap*MHt+BRW}
			PosMenu(ctl,Mb.CCn,STp,SLt)}
		Mb=Mb.PrvMbr;
		if(Mb){	MStl=!Nav4?Mb.style:Mb;PadL=Mb.value.indexOf("<")==-1?ctl.LftXtra:0;
			PadT=Mb.value.indexOf("<")==-1?ctl.TpXtra:0;
			MWt=!Nav4?parseInt(MStl.width)+PadL:MStl.clip.width;
			MHt=!Nav4?parseInt(MStl.height)+PadT:MStl.clip.height;
			Hi?Li-=BTWn?(MWt+BRW):(MWt):Ti-=BTWn?(MHt+BRW):MHt}}
	status=statusText;ctl.RLvl--}

function StMnu(ctl){
	if(!ctl.Crtd)return;
	var i,Pntr=ctl.FrstCntnr,Str=ctl.ScLoc.SetMenu?ctl.ScLoc.SetMenu:"0";
	while(Str.indexOf("_")!=-1&&ctl.RememberStatus==1){
		i=Pntr.NrItms-parseInt(Str.substring(0,Str.indexOf("_")));
		Str=Str.slice(Str.indexOf("_")+1);
		Pntr=Pntr.FrstMbr;
		for(i;i;i--)Pntr=Pntr.PrvMbr;
		if(Nav4)Pntr.CLyr.OM();
		else Pntr.OM();
		Pntr=Pntr.CCn
	}
	i=Pntr.NrItms-parseInt(Str);
	Pntr=Pntr.FrstMbr;
	for(i;i;i--)Pntr=Pntr.PrvMbr;
	if(ctl.RmbrNow!=null){SetItem(ctl.RmbrNow,0);ctl.RmbrNow.Clckd=0}
	if(Pntr!=null){SetItem(Pntr,1);Pntr.Clckd=1;
	if(ctl.RememberStatus==1){if(Nav4)Pntr.CLyr.OM();else Pntr.OM()}}
	ctl.RmbrNow=Pntr;
	ClrAllChlds(ctl,ctl.FrstCntnr.FrstMbr);
	Rmbr(ctl,ctl.FrstCntnr)}

function InitiateByID(ctlID){
	Initiate(document.all[ctlID]);
}

function Initiate(ctl){
	if(ctl.IniFlg&&ctl.Ldd){
		Init(ctl,ctl.FrstCntnr);
		ctl.IniFlg=0;
		if(ctl.RememberStatus)Rmbr(ctl,ctl.FrstCntnr);
		if(ctl.ShwFlg)AfterCloseAll();ctl.ShwFlg=0;showAllWindowed(ctl);
	}
}

function Rmbr(ctl,CntPtr){
	var Mbr=CntPtr.FrstMbr,St;
	while(Mbr!=null){
		if(Mbr.DoRmbr){
			HiliteItem(ctl,Mbr);
			if(Mbr.CCn&&ctl.RememberStatus==1){St=Nav4?Mbr.CCn:Mbr.CCn.style;St.visibility=ctl.M_Show;Rmbr(ctl,Mbr.CCn)}
			break}
		else Mbr=Mbr.PrvMbr}}

function Init(ctl,CPt){
	var Mb=CPt.FrstMbr,MCSt=Nav4?CPt:CPt.style;
	ctl.RLvl++;
	MCSt.visibility=ctl.RLvl==1?ctl.M_Show:ctl.M_Hide;
	CPt.Shw=ctl.RLvl==1?1:0;
	while(Mb!=null){
		if(Mb.Hilite) {
			LowItem(Mb);
		}
		if(Mb.CCn)Init(ctl,Mb.CCn);
		Mb=Mb.PrvMbr
	}
	ctl.RLvl--
}

function ClrAllChlds(ctl,Pt){
	var PSt,Pc;
	while(Pt){
		if(Pt.Hilite){
			Pc=Nav4?Pt.CLyr:Pt;
			if(Pc!=ctl.CurOvr){
				LowItem(Pt)
			}
			if(Pt.CCn){
				PSt=Nav4?Pt.CCn:Pt.CCn.style;
				if(Pc!=ctl.CurOvr){
					PSt.visibility=ctl.M_Hide;Pt.CCn.Shw=0
				}
				ClrAllChlds(ctl,Pt.CCn.FrstMbr)
			}
			break;
		}
		Pt=Pt.PrvMbr
	}
}

function SetItem(Pntr,x){while(Pntr!=null){Pntr.DoRmbr=x;Pntr=Nav4?Pntr.CLyr.Ctnr.Cllr:Pntr.Ctnr.Cllr}}

function GoTo(ctl){
	if (typeof(ctl) == "undefined") {
		if(event != null) {
			ctl = getCtl(event.srcElement);
		}
		else{
			ctl = currentMenu;
		}
	}
	var HP=Nav4?this.LLyr:this;
	
	/* imagenes del menu, al parecer no esta funcionando
	var nivel = HP.Nvl;
		
	if(ctl.FirstLineHorizontal==0 || ctl.AnchoSubMenu=="fijo"){
	
		//volver a imagen normal un menu anteriormente seleccionado
		if(ctl.earlierNode != undefined){
			if(ctl.earlierNode.level==0 && ctl.ImgMenuNormal1st){
				ctl.earlierNode.style.backgroundImage="url('" + ctl.ImgMenuNormal1st + "')";
			}
			else if(ctl.earlierNode.level!=0 && ctl.ImgMenuSelec2nd){
				ctl.earlierNode.style.backgroundImage="url('" + ctl.ImgMenuNormal2nd + "')";
			}
		}
	
		// seteo el nodo actual seleccionado del menu donde hay que colocar la imagen y se pone la imagen
		if(nivel!=0){
			ctl.currentNode=ctl.HiNode;
			if(ctl.ImgMenuSelec2nd){
				ctl.currentNode.style.backgroundImage="url('" + ctl.ImgMenuSelec2nd + "')";
			}
		}
		else {
			ctl.currentNode=HP;
			if(ctl.ImgMenuSelec1st){
				ctl.currentNode.style.backgroundImage="url('" + ctl.ImgMenuSelec1st + "')";
			}
		}
	
		ctl.earlierNode=ctl.currentNode;
		ctl.earlierNode.level=nivel;
		
	}
	*/
	
	if(HP.Arr[1]){
		status=statusText;
		//LowItem(HP);
		ctl.IniFlg=1;
		Initiate(ctl);
		HP.Arr[1].indexOf("javascript:")!=-1?eval(HP.Arr[1]):ctl.DcLoc.location.href=ctl.BaseHref+HP.Arr[1]
	}
}

function HiliteItem(ctl,P){
	
	if(Nav4){	
	
		if(P.ro)P.document.images[P.rid].src=P.ri2;
		/* no soporta estilos
		else{	
			if(P.Arr[7]&&!P.Arr[2])P.bgColor=P.Arr[7];
			if(P.value.indexOf("<img")==-1){
				P.document.write(P.Ovalue);P.document.close()
			}
		}
		*/
	}
	else{	
		if(P.ro){
			var Lc=P.Lvl==1?ctl.FLoc:ctl.ScLoc;Lc.document.images[P.rid].src=P.ri2;
		}
		else{
	
			if(P.Arr[10]&&!P.Arr[2]) applyClassNameRecursive(P,P.Arr[10]);
			//stylesheet if(P.Arr[7]&&!P.Arr[2])P.bgColor=P.Arr[7]; 
			//if(P.Arr[9])P.style.color=P.Arr[9];
		}
	}
	P.Hilite=1
}

function LowItem(P){
	P.Hilite=0;
	if(P.ro){
		if(Nav4)P.document.images[P.rid].src=P.ri1;
		else{
			var Lc=P.Lvl==1?ctl.FLoc:ctl.ScLoc;Lc.document.images[P.rid].src=P.ri1
		}
	}
	else{
		if(Nav4){
			/* no soporta estilos
			if(P.Arr[6]&&!P.Arr[2])P.bgColor=P.Arr[6];
			if(P.value.indexOf("<img")==-1){
				P.document.write(P.value);P.document.close();
			}
			*/
			
		}
		else{
			//stylesheet if(P.Arr[6]&&!P.Arr[2])P.style.backgroundColor=P.Arr[6];
			if(P.Arr[9]&&!P.Arr[2]) applyClassNameRecursive(P,P.Arr[9]);
			//if(P.Arr[6]&&!P.Arr[2])P.style.backgroundColor=P.Arr[6];
			//if(P.Arr[8])P.style.color=P.Arr[8];
		}
	}
}

function OpenMenu(ctl){
	if (typeof(ctl) == "undefined") {
		if(event != null) {
			ctl = getCtl(event.srcElement);
		}
		else{
			ctl = currentMenu;
		}
	}
	ctlID=ctl.id;
	if(!ctl.Ldd||!ctl.Crtd)return;
	if(ctl.OpnTmr)clearTimeout(ctl.OpnTmr);
	var P=Nav4?this.LLyr:this;
	var entrar=0;
	
	if(P.Nvl==1){ctl.HiNode=P;}
	if (P.NofChlds&&!P.CCn) entrar = 1;
	if (P.CCn) if (P.CCn.innerHTML=="")	entrar = 1;

	if(entrar){
		ctl.RLvl=this.Lvl;
		P.CCn=CreateMenuStructure(ctl,P.MN+"_",P.NofChlds,P);
		var Ti,Li,Hi;
		var MStl=!Nav4?P.style:P;
		var PadL=P.value.indexOf("<")==-1?ctl.LftXtra:0;
		var PadT=P.value.indexOf("<")==-1?ctl.TpXtra:0;
		var MWt=!Nav4?parseInt(MStl.width)+PadL:MStl.clip.width;
		var MHt=!Nav4?parseInt(MStl.height)+PadT:MStl.clip.height;
		var CCw,CCh,STp,SLt;
		var BRW=ctl.RLvl==1?ctl.BorderWidthMain:ctl.BorderWidthSub;
		if(ctl.RightToLeft)CCw=Nav4?P.CCn.clip.width:parseInt(P.CCn.style.width);
		if(ctl.BottomUp)CCh=Nav4?P.CCn.clip.height:parseInt(P.CCn.style.height);
		if(ctl.RLvl==1&&ctl.FirstLineHorizontal){Hi=1;Li=(Nav4?P.left:parseInt(P.style.left))-BRW;Ti=0}
		else{Hi=Li=0;Ti=(Nav4?P.top:parseInt(P.style.top))-BRW}
		if(Hi){STp=ctl.BottomUp?Ti-CCh:Ti+MHt+2*BRW;SLt=ctl.RightToLeft?Li+MWt-CCw:Li}
		else{SLt=ctl.RightToLeft?Li-CCw+ctl.ChildOverlap*MWt+BRW:Li+(1-ctl.ChildOverlap)*MWt;
		STp=ctl.RLvl==1&&ctl.AcrssFrms?ctl.BottomUp?Ti-CCh+MHt:Ti:ctl.BottomUp?Ti-CCh+(1-ctl.ChildVerticalOverlap)*MHt+2*BRW:Ti+ctl.ChildVerticalOverlap*MHt+BRW}
		PosMenu(ctl,P.CCn,STp,SLt);
		ctl.RLvl=0}
		
	var CCnt=Nav4?this.LLyr.CCn:this.CCn,HP=Nav4?this.LLyr:this;
	ctl.CurOvr=this;ctl.IniFlg=0;ClrAllChlds(ctl,this.Ctnr.FrstMbr);
	
	if(!HP.Hilite){
		HiliteItem(ctl,HP);
	}
	//if(CCnt!=null&&!CCnt.Shw)ctl.RememberStatus?Unfld:ctl.OpnTmr=setTimeout("Unfld(document.all[" + ctlID + "])",ctl.UnfoldDelay);
	//if(CCnt!=null&&!CCnt.Shw)ctl.RememberStatus?Unfld:ctl.OpnTmr=setTimeout(Unfld,ctl.UnfoldDelay,ctlID);
	//if(CCnt!=null&&!CCnt.Shw)ctl.RememberStatus?Unfld:ctl.OpnTmr=setTimeout(Unfld,ctl.UnfoldDelay,""+ctl.id+"");
	if(CCnt!=null&&!CCnt.Shw)ctl.RememberStatus?Unfld:ctl.OpnTmr=setTimeout("Unfld('"+ctl.id+"')",ctl.UnfoldDelay);
	/* status=HP.Arr[16]*/
	status=HP.Arr[1]
}

function Unfld(ctlID){
	ctl = document.all[ctlID];
	var P=ctl.CurOvr;
	/* Linea original, antes de ser modificada para el scroll
		var TS=ExpYes?ctl.ScLoc.document.body.scrollTop:ctl.ScLoc.pageYOffset,LS=ExpYes?ctl.ScLoc.document.body.scrollLeft:ctl.ScLoc.pageXOffset,CCnt=Nav4?P.LLyr.CCn:P.CCn,THt=Nav4?P.clip.height:parseInt(P.style.height),TWt=Nav4?P.clip.width:parseInt(P.style.width),TLt=ctl.AcrssFrms&&P.Lvl==1&&!FirstLineHorizontal?0:Nav4?P.Ctnr.left:parseInt(P.Ctnr.style.left),TTp=ctl.AcrssFrms&&P.Lvl==1&&FirstLineHorizontal?0:Nav4?P.Ctnr.top:parseInt(P.Ctnr.style.top); */
	var TS=ExpYes?ctl.ScLoc.document.body.scrollTop:ctl.ScLoc.pageYOffset,LS=ExpYes?ctl.ScLoc.document.body.scrollLeft:ctl.ScLoc.pageXOffset,CCnt=Nav4?P.LLyr.CCn:P.CCn,THt=Nav4?P.clip.height:parseInt(P.style.height),TWt=Nav4?P.clip.width:parseInt(P.style.width),TLt=ctl.AcrssFrms&&P.Lvl==1&&!ctl.FirstLineHorizontal?0:Nav4?P.Ctnr.left:parseInt(P.Ctnr.style.left),TTp=ctl.AcrssFrms&&P.Lvl==1&&ctl.FirstLineHorizontal?0:Nav4?P.Ctnr.top:parseInt(P.Ctnr.style.top);
	var CCW=Nav4?P.LLyr.CCn.clip.width:parseInt(P.CCn.style.width),CCH=Nav4?P.LLyr.CCn.clip.height:parseInt(P.CCn.style.height),CCSt=Nav4?P.LLyr.CCn:P.CCn.style,SLt=ctl.AcrssFrms&&P.Lvl==1?CCnt.OrgLeft+TLt+LS:CCnt.OrgLeft+TLt,STp=ctl.AcrssFrms&&P.Lvl==1?CCnt.OrgTop+TTp+TS+CurrentStartTop-ctl.StartTop:CCnt.OrgTop+TTp;
	if(!ctl.ShwFlg){ctl.ShwFlg=1;BeforeFirstOpen()}
	if(ctl.MenuWrap){
		if(ctl.RightToLeft){if(SLt<LS)SLt=P.Lvl==1?LS:SLt+(CCW+(1-2*ctl.ChildOverlap)*TWt);if(SLt+CCW>ctl.SWinW+LS)SLt=ctl.SWinW+LS-CCW}
		else{if(SLt+CCW>ctl.SWinW+LS)SLt=P.Lvl==1?ctl.SWinW+LS-CCW:SLt-(CCW+(1-2*ctl.ChildOverlap)*TWt);if(SLt<LS)SLt=LS}
		if(ctl.BottomUp){if(STp<TS)STp=P.Lvl==1?TS:STp+(CCH-(1-2*ctl.ChildVerticalOverlap)*THt);if(STp+CCH>ctl.SWinH+TS)STp=ctl.SWinH+TS-CCH+(Nav4?4:0)}
		else{if(STp+CCH>TS+ctl.SWinH)STp=P.Lvl==1?STp=TS+ctl.SWinH-CCH:STp-CCH+(1-2*ctl.ChildVerticalOverlap)*THt;if(STp<TS)STp=TS}}
	CCSt.top=STp+P_X;CCSt.left=SLt+P_X;
	//if(Fltr&&MenuSlide){P.CCn.filters[0].Apply();P.CCn.filters[0].play()}
	CCSt.zIndex = 105;
	CCSt.visibility=ctl.M_Show;
	hideAllWindowed(ctl);
	}

function OpenMenuClick(ctl){
	if (typeof(ctl) == "undefined") {
		if(event != null) {
			ctl = getCtl(event.srcElement);
		}
		else{
			ctl = currentMenu;
		}
	}
	if(!ctl.Ldd||!ctl.Crtd)return;
	var HP=Nav4?this.LLyr:this;ctl.CurOvr=this;
	ctl.IniFlg=0;ClrAllChlds(ctl,this.Ctnr.FrstMbr);HiliteItem(ctl,HP);status=HP.Arr[1]}

function CloseMenu(ctl){
	if (typeof(ctl) == "undefined") {
		if(event != null) {
			ctl = getCtl(event.srcElement);
		}
		else{
			ctl = currentMenu;
		}
	}
	ctlID=ctl.id;
	if(!ctl.Ldd||!ctl.Crtd)return;
	status=statusText;
	if(this==ctl.CurOvr){
		if(ctl.OpnTmr)clearTimeout(ctl.OpnTmr);
		if(ctl.CloseTmr)clearTimeout(ctl.CloseTmr);
		ctl.IniFlg=1;

		//ctl.CloseTmr=setTimeout("Initiate(document.all[" + ctlID + "],document.all[" + ctlID + "].CurOvr)",ctl.DissapearDelay)
		ctl.CloseTmr=setTimeout("InitiateByID('" + ctlID + "')",ctl.DissapearDelay)
	}
}

function CntnrSetUp(ctl,W,H,NoOff,WMu,Mc){
	/*var x=eval(ctl.id+"."+WMu+"[10]")!=""?eval(ctl.id+"."+WMu+"[10]"):ctl.BorderColor;*/
	
	this.FrstMbr=null;this.NrItms=NoOff;this.Cllr=Mc;this.Shw=0;
	this.OrgLeft=this.OrgTop=0;
	if(Nav4){/*if(x)this.bgColor=x;*/this.visibility="hide";this.resizeTo(W,H)}
	else{applyClassNameRecursive(this,"MenuBorder");/*if(x)this.style.backgroundColor=x;*/this.style.width=W+P_X;this.style.height=H+P_X;
		if(!NavYes)this.style.zIndex=ctl.RLvl+Ztop;
		if(Fltr){ctl.FStr="";if(ctl.MenuSlide&&ctl.RLvl!=1)ctl.FStr=ctl.MenuSlide;if(ctl.MenuShadow)ctl.FStr+=ctl.MenuShadow;
			if(ctl.MenuOpacity)ctl.FStr+=ctl.MenuOpacity;if(ctl.FStr!="")this.style.filter=ctl.FStr}}}

function MbrSetUp(ctl,MbC,PrMmbr,WMu,Wd,Ht,Nofs){

	//aqui asigna el texto al elemento
	var nivel = eval(ctl.id+"."+WMu+"[6]");
	var firstChild = eval(ctl.id+"."+WMu+"[7]");
	var lastChild = eval(ctl.id+"."+WMu+"[8]");
	//var MenuTextCentered;
	
	//if(nivel==0) {
	//	MenuTextCentered = ctl.MenuTextCentered1st;
	//}
	//else if(nivel==1) {
	//	MenuTextCentered = ctl.MenuTextCentered2nd;
	//}
	//else {
	//	MenuTextCentered = ctl.MenuTextCentered;
	//}
	
	var img = getImg(ctl,MbC,nivel,firstChild,lastChild);
	
	//var Tfld = "<SPAN class=\"" + this.Arr[20] + "\"><TABLE cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\" ><TR><TD width=\"0%\" >"+img+"</TD><TD width=\"100%\" align='" + MenuTextCentered + "'>"+this.Arr[0]+"</TD></TR></TABLE></SPAN>";
	var Tfld = "<TABLE class=\"" + this.Arr[9] + "\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\" ><TR height=" + Ht+ "><TD width=\"0%\" >"+img+"</TD><TD width=\"100%\">"+this.Arr[0]+"</TD></TR></TABLE>";
	//var Tfld=this.Arr[0];
	
	var Lctn=ctl.RLvl==1?ctl.FLoc:ctl.ScLoc,t,T,L,W,H,S,a;
	this.PrvMbr=PrMmbr;this.Lvl=ctl.RLvl;this.Ctnr=MbC;this.CCn=null;this.ai=null;this.Hilite=0;this.DoRmbr=0;
	this.Clckd=0;this.OM=OpenMenu;this.style.overflow="hidden";
	this.MN=WMu;this.NofChlds=Nofs;
	this.style.cursor=(this.Arr[1]||(ctl.RLvl==1&&ctl.UnfoldsOnClick))?ExpYes?"hand":"pointer":"default";this.ro=0;
	
	if(Tfld.indexOf("rollover")!=-1){this.ro=1;this.ri1=Tfld.substring(Tfld.indexOf("?")+1,Tfld.lastIndexOf("?"));
		this.ri2=Tfld.substring(Tfld.lastIndexOf("?")+1,Tfld.length);this.rid=WMu+"i";
		Tfld="<img src=\""+this.ri1+"\" name=\""+this.rid+"\" width=\""+Wd+"\" height=\""+Ht+"\">"}
	
	this.value=Tfld;
	
	//this.className = this.Arr[21];
	//this.style.color=this.Arr[8];this.style.fontFamily=this.Arr[11];this.style.fontSize=!Mac4?this.Arr[12]+"pt":Math.round(4*this.Arr[12]/3)+"pt";
	//this.style.fontWeight=this.Arr[13]?"bold":"normal";this.style.fontStyle=this.Arr[14]?"italic":"normal";
	//if(this.Arr[6]){this.style.backgroundColor=this.Arr[6];this.style.textAlign=this.Arr[15];}
	
	if(this.Arr[2]){this.style.backgroundImage="url(\""+this.Arr[2]+"\")";}
	
	if(ctl.FirstLineHorizontal==0 || ctl.AnchoSubMenu=="fijo"){
		if(nivel==0 && ctl.ImgMenuNormal1st){this.style.backgroundImage="url('" + ctl.ImgMenuNormal1st + "')";}
		if(nivel==1 && ctl.ImgMenuNormal2nd){this.style.backgroundImage="url('" + ctl.ImgMenuNormal2nd + "')";}
	}
	
	if(Tfld.indexOf("<")==-1){this.style.width=Wd-ctl.LftXtra+P_X;this.style.height=Ht-ctl.TpXtra+P_X;this.style.paddingLeft=ctl.LeftPadding+P_X;this.style.paddingTop=ctl.TopPadding+P_X}
	else{this.style.width=Wd+P_X;this.style.height=Ht+P_X}
	if(Tfld.indexOf("<")==-1&&DomYes){t=Lctn.document.createTextNode(Tfld);this.appendChild(t)}
	else this.innerHTML=Tfld;
	if(this.Arr[3]){a=ctl.RLvl==1&&ctl.FirstLineHorizontal?ctl.BottomUp?9:3:ctl.RightToLeft?6:0;
		if(Arrws[a]!=""){S=Arrws[a];W=Arrws[a+1];H=Arrws[a+2];T=ctl.RLvl==1&&ctl.FirstLineHorizontal?ctl.BottomUp?2:Ht-H-2:(Ht-H)/2;L=ctl.RightToLeft?2:Wd-W-2;
			if(DomYes){
				t=Lctn.document.createElement("img");
				this.appendChild(t);
				t.style.position="absolute";
				t.src=S;t.style.width=W+P_X;
				t.style.height=H+P_X;
				//if(ctl.FirstLineHorizontal==1 && ctl.MenuTextCentered2nd!="left" && nivel<=1){L=0}
				t.style.top=T+P_X;t.style.left=L+P_X;
			}
			else{Tfld+="<div id=\""+WMu+"_im\" style=\"position:absolute; top:"+T+"; left:"+L+"; width:"+W+"; height:"+H+";visibility:inherit\"><img src=\""+S+"\"></div>";
				this.innerHTML=Tfld;t=Lctn.document.all[WMu+"_im"]}
			this.ai=t}}
	if(ExpYes){this.onselectstart=CnclSlct;this.onmouseover=ctl.RLvl==1&&ctl.UnfoldsOnClick?OpenMenuClick:OpenMenu;
		this.onmouseout=CloseMenu;this.onclick=ctl.RLvl==1&&ctl.UnfoldsOnClick&&this.Arr[3]?OpenMenu:GoTo}
	else{ctl.RLvl==1&&ctl.UnfoldsOnClick?this.addEventListener("mouseover",OpenMenuClick(ctl),false):this.addEventListener("mouseover",OpenMenu(ctl),false);
		this.addEventListener("mouseout",CloseMenu,false);
		ctl.RLvl==1&&ctl.UnfoldsOnClick&&this.Arr[3]?this.addEventListener("click",OpenMenu(ctl),false):this.addEventListener("click",GoTo(ctl),false)}

	
}

function NavMbrSetUp(ctl,MbC,PrMmbr,WMu,Wd,Ht,Nofs){
	//aqui asigna el texto al elemento
	var nivel = eval(ctl.id+"."+WMu+"[6]");
	var firstChild = eval(ctl.id+"."+WMu+"[7]");
	var lastChild = eval(ctl.id+"."+WMu+"[8]");
	//var MenuTextCentered;
	if(PrMmbr!=null){PrMmbr.Nvl = nivel;}
	//if(nivel==0) {
	//	MenuTextCentered = ctl.MenuTextCentered1st;
	//}
	//else if(nivel==1) {
	//	MenuTextCentered = ctl.MenuTextCentered2nd;
	//}
	//else {
	//	MenuTextCentered = ctl.MenuTextCentered;
	//}
	
	var img = getImg(ctl,MbC,nivel,firstChild,lastChild);
	
	//var Tfld = "<TABLE cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\" ><TR><TD width=\"0%\" >"+img+"</TD><TD width=\"100%\" align='" + MenuTextCentered + "'>"+this.Arr[0]+"</TD></TR></TABLE>";
	var Tfld = "<TABLE class=\"" + this.Arr[9] + "\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\" ><TR height=" + Ht+ "><TD width=\"0%\" >"+img+"</TD><TD width=\"100%\">"+this.Arr[0]+"</TD></TR></TABLE>";
	//var Tfld=this.Arr[0];
	
	this.ro=0;
	if(this.value.indexOf("rollover")!=-1){
		this.ro=1;this.ri1=this.value.substring(this.value.indexOf("?")+1,this.value.lastIndexOf("?"));
		this.ri2=this.value.substring(this.value.lastIndexOf("?")+1,this.value.length);this.rid=WMu+"i";
		this.value="<img src=\""+this.ri1+"\" name=\""+this.rid+"\">"}
	/*
	ctl.CntrTxt=this.Arr[15]!="left"?"<div align=\""+this.Arr[15]+"\">":"";
	ctl.TxtClose="</font>"+this.Arr[15]!="left"?"</div>":"";
	if(ctl.LeftPadding&&this.value.indexOf("<")==-1&&this.Arr[15]=="left")this.value="&nbsp\;"+this.value;
	if(this.Arr[13])this.value=this.value.bold();if(this.Arr[14])this.value=this.value.italics();
	this.Ovalue=this.value;this.value=this.value.fontcolor(this.Arr[8]);
	this.Ovalue=this.Ovalue.fontcolor(this.Arr[9]);
	this.value=ctl.CntrTxt+"<font face=\""+this.Arr[11]+"\" point-size=\""+(!Mac4?this.Arr[12]:Math.round(4*this.Arr[12]/3))+"\">"+this.value+ctl.TxtClose;
	this.Ovalue=ctl.CntrTxt+"<font face=\""+this.Arr[11]+"\" point-size=\""+(!Mac4?this.Arr[12]:Math.round(4*this.Arr[12]/3))+"\">"+this.Ovalue+ctl.TxtClose;
	*/
	this.CCn=null;this.PrvMbr=PrMmbr;this.Hilite=0;this.DoRmbr=0;this.Clckd=0;this.visibility="inherit";
	this.MN=WMu;this.NofChlds=Nofs;
	//stylesheet if(this.Arr[6])this.bgColor=this.Arr[6];this.resizeTo(Wd,Ht);
	if(this.Arr[9])this.className=this.Arr[9];this.resizeTo(Wd,Ht); 
	if(!ctl.AcrssFrms&&this.Arr[2])this.background.src=this.Arr[2];
	this.document.write(this.value);this.document.close();
	this.CLyr=new Layer(Wd,MbC);
	this.CLyr.Lvl=ctl.RLvl;this.CLyr.visibility="inherit";
	this.CLyr.onmouseover=ctl.RLvl==1&&ctl.UnfoldsOnClick?OpenMenuClick:OpenMenu;this.CLyr.onmouseout=CloseMenu;
	this.CLyr.captureEvents(Event.MOUSEUP);this.CLyr.onmouseup=ctl.RLvl==1&&ctl.UnfoldsOnClick&&this.Arr[3]?OpenMenu:GoTo;
	this.CLyr.OM=OpenMenu;
	this.CLyr.LLyr=this;this.CLyr.resizeTo(Wd,Ht);this.CLyr.Ctnr=MbC;
	if(this.Arr[3]){a=ctl.RLvl==1&&ctl.FirstLineHorizontal?ctl.BottomUp?9:3:ctl.RightToLeft?6:0;
		if(Arrws[a]!=""){this.CLyr.ILyr=new Layer(Arrws[a+1],this.CLyr);this.CLyr.ILyr.visibility="inherit";
			this.CLyr.ILyr.top=ctl.RLvl==1&&ctl.FirstLineHorizontal?ctl.BottomUp?2:Ht-Arrws[a+2]-2:(Ht-Arrws[a+2])/2;
			this.CLyr.ILyr.left=ctl.RightToLeft?2:Wd-Arrws[a+1]-2;this.CLyr.ILyr.width=Arrws[a+1];this.CLyr.ILyr.height=Arrws[a+2];
			ctl.ImgStr="<img src=\""+Arrws[a]+"\" width=\""+Arrws[a+1]+"\" height=\""+Arrws[a+2]+"\">";
			this.CLyr.ILyr.document.write(ctl.ImgStr);this.CLyr.ILyr.document.close()}}}

function CreateMenuStructure(ctl,MNm,No,Mcllr){
	
	status="Building menu";ctl.RLvl++;
	var i,NOs,Mbr,W=0,H=0,PMb=null,WMnu=MNm+"1",MWd=eval(ctl.id+"."+WMnu+"[5]"),MHt=eval(ctl.id+"."+WMnu+"[4]"),Lctn=ctl.RLvl==1?ctl.FLoc:ctl.ScLoc;
	var BRW=ctl.RLvl==1?ctl.BorderWidthMain:ctl.BorderWidthSub,BTWn=ctl.RLvl==1?ctl.BorderBtwnMain:ctl.BorderBtwnSub;
	if(ctl.RLvl==1&&ctl.FirstLineHorizontal){
		for(i=1;i<No+1;i++){WMnu=MNm+eval(i);W=eval(ctl.id+"."+WMnu+"[5]")?W+eval(ctl.id+"."+WMnu+"[5]"):W+MWd}
		W=BTWn?W+(No+1)*BRW:W+2*BRW;H=MHt+2*BRW}
	else{for(i=1;i<No+1;i++){WMnu=MNm+eval(i);H=eval(ctl.id+"."+WMnu+"[4]")?H+eval(ctl.id+"."+WMnu+"[4]"):H+MHt}
		H=BTWn?H+(No+1)*BRW:H+2*BRW;W=MWd+2*BRW}
	if(DomYes){
	// Añadido por el tema del scroll
		if(MNm=="Menu" && ctl.MenuScroll) { 
			MbC = scroll_build(ctl,Lctn);
		}
		else {
			var MbC=Lctn.document.createElement("div");
			MbC.ctl=ctl;
			MbC.style.position="absolute";
			MbC.style.visibility="hidden";
			Lctn.document.body.appendChild(MbC);
		}
	}
	else{if(Nav4)var MbC=new Layer(W,Lctn);
		else{WMnu+="c";Lctn.document.body.insertAdjacentHTML("AfterBegin","<div id=\""+ctl.id+"."+WMnu+"\" style=\"visibility:hidden; position:absolute;\"><\/div>");
			var MbC=Lctn.document.all[ctl.id+"."+WMnu]}}
	
	MbC.SetUp=CntnrSetUp;MbC.SetUp(ctl,W,H,No,MNm+"1",Mcllr);
	if(Exp4){MbC.InnerString="";
		for(i=1;i<No+1;i++){WMnu=MNm+eval(i);MbC.InnerString+="<div id=\""+ctl.id+"."+WMnu+"\" style=\"position:absolute;\"><\/div>"}
		MbC.innerHTML=MbC.InnerString}
	
	if(MNm=="Menu" && ctl.MenuScroll) { ctl.divMen.style.height = 0; }
	for(i=1;i<No+1;i++){WMnu=MNm+eval(i);NOs=eval(ctl.id+"."+WMnu+"[3]");
		W=ctl.RLvl==1&&ctl.FirstLineHorizontal?eval(ctl.id+"."+WMnu+"[5]")?eval(ctl.id+"."+WMnu+"[5]"):MWd:MWd;
		H=ctl.RLvl==1&&ctl.FirstLineHorizontal?MHt:eval(ctl.id+"."+WMnu+"[4]")?eval(ctl.id+"."+WMnu+"[4]"):MHt;
		if(DomYes){Mbr=Lctn.document.createElement("div");	Mbr.style.position="absolute";Mbr.style.visibility="inherit";MbC.appendChild(Mbr)}
		else Mbr=Nav4?new Layer(W,MbC):Lctn.document.all[ctl.id+"."+WMnu];
		
		Mbr.Arr=eval(ctl.id+"."+WMnu);
		/*
		if(Mbr.Arr[6]=="")Mbr.Arr[6]=ctl.LowBgColor;if(Mbr.Arr[7]=="")Mbr.Arr[7]=ctl.HighBgColor;if(Mbr.Arr[8]=="")Mbr.Arr[8]=ctl.FontLowColor;
		if(Mbr.Arr[9]=="")Mbr.Arr[9]=ctl.FontHighColor;if(Mbr.Arr[11]=="")Mbr.Arr[11]=ctl.FontFamily;if(Mbr.Arr[12]==-1)Mbr.Arr[12]=ctl.FontSize;
		if(Mbr.Arr[13]==-1)Mbr.Arr[13]=ctl.FontBold;if(Mbr.Arr[14]==-1)Mbr.Arr[14]=ctl.FontItalic;
		*/
		/*if(Mbr.Arr[15]=="")Mbr.Arr[15]=ctl.MenuTextCentered;if(Mbr.Arr[16]=="")Mbr.Arr[16]=Mbr.Arr[1];*/
		
		var aa;
		if((PMb != null) && (PMb.innerText == "Prueba2")){aa=0;}
		Mbr.SetUp=Nav4?NavMbrSetUp:MbrSetUp;Mbr.SetUp(ctl,MbC,PMb,WMnu,W,H,NOs);
		if(NOs&&!ctl.BuildOnDemand){Mbr.CCn=CreateMenuStructure(ctl,WMnu+"_",NOs,Mbr)}
		PMb=Mbr;
		
		var nivel = eval(ctl.id+"."+WMnu+"[17]");
		if(PMb!=null){PMb.Nvl=nivel;}
		
		// Añadido por el tema del scroll
		if(MNm=="Menu" && ctl.MenuScroll) { ctl.divMen.style.height = (parseInt(ctl.divMen.style.height) + parseInt(Mbr.style.height) + 1) + P_X; }
		}
	MbC.FrstMbr=Mbr;
	ctl.RLvl--;
	
	// Añadido por el tema del scroll
	if(MNm=="Menu" && ctl.MenuScroll){ scroll_RePos(ctl);do_scroll(ctl,0);}
	
	return(MbC)}

function CreateMenuStructureAgain(ctl,MNm,No){
	if(!ctl.BuildOnDemand){
		var i,WMnu,NOs,PMb,Mbr=ctl.FrstCntnr.FrstMbr;ctl.RLvl++;
		for(i=No;i>0;i--){WMnu=MNm+eval(i);NOs=eval(ctl.id+"."+WMnu+"[3]");PMb=Mbr;if(NOs)Mbr.CCn=CreateMenuStructure(ctl,WMnu+"_",NOs,Mbr);Mbr=Mbr.PrvMbr}
		ctl.RLvl--}
	else{	var Mbr=ctl.FrstCntnr.FrstMbr;
		while(Mbr){Mbr.CCn=null;Mbr=Mbr.PrvMbr}}}

// Minimizar este código		
function hideAllWindowed(ctl) {
//IFRAME, SELECT, OBJECT, APPLET, EMBED
	
	var aWindowedElement;
	var Loc=ctl.AcrssFrms?ctl.ScLoc:ctl.FLoc;

	aWindowedElement = Loc.document.body.getElementsByTagName("SELECT");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "hidden";
    //aWindowedElement = Loc.document.body.getElementsByTagName("IFRAME");
    //for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "hidden";
    aWindowedElement = Loc.document.body.getElementsByTagName("OBJECT");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "hidden";
    aWindowedElement = Loc.document.body.getElementsByTagName("APPLET");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "hidden";
    aWindowedElement = Loc.document.body.getElementsByTagName("EMBED");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "hidden";
}

function showAllWindowed(ctl) {
	
	var aWindowedElement;
	var Loc=ctl.AcrssFrms?ctl.ScLoc:ctl.FLoc;
	
	aWindowedElement = Loc.document.body.getElementsByTagName("SELECT");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "visible";
    //aWindowedElement = Loc.document.body.getElementsByTagName("IFRAME");
    //for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "visible";
    aWindowedElement = Loc.document.body.getElementsByTagName("OBJECT");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "visible";
    aWindowedElement = Loc.document.body.getElementsByTagName("APPLET");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "visible";
    aWindowedElement = Loc.document.body.getElementsByTagName("EMBED");
    for (i=0; i<aWindowedElement.length; i++) aWindowedElement.item(i).style.visibility = "visible";
}

///////// Scroll
scroll_nav = (document.layers) ? true : false;
scroll_iex = (document.all) ? true : false;

////////////// cache de imagenes
scroll_img = new Array();
scroll_img["scroll_down"] = new Image(22,22);
scroll_img["scroll_down_d"] = new Image(22,22);
scroll_img["scroll_up"] = new Image(22,22);
scroll_img["scroll_up_d"] = new Image(22,22);
scroll_img["scroll_start"] = new Image(22,22);
scroll_img["scroll_start_d"] = new Image(22,22);
scroll_img["scroll_end"] = new Image(22,22);
scroll_img["scroll_end_d"] = new Image(22,22);

scroll_img["scroll_down"].src = "img/menu/scroll_down.gif";
scroll_img["scroll_down_d"].src = "img/menu/scroll_down_d.gif";
scroll_img["scroll_up"].src = "img/menu/scroll_up.gif";
scroll_img["scroll_up_d"].src = "img/menu/scroll_up_d.gif";
scroll_img["scroll_start"].src = "img/menu/scroll_start.gif";
scroll_img["scroll_start_d"].src = "img/menu/scroll_start_d.gif";
scroll_img["scroll_end"].src = "img/menu/scroll_end.gif";
scroll_img["scroll_end_d"].src = "img/menu/scroll_end_d.gif";

function scroll_up_ByID(ctlID) {
	scroll_up(document.all[ctlID]);
}

function scroll_up(ctl) {
	ctlID = ctl.id;
	if(ctl.scroll_pos > ctl.scroll_upr) ctl.scroll_pos -= ctl.scroll_stp;
	if(ctl.scroll_pos < ctl.scroll_upr) ctl.scroll_pos = ctl.scroll_upr;
	do_scroll(ctl,ctl.scroll_pos);  
	ctl.scroll_tim = setTimeout("scroll_up_ByID('" + ctlID + "')", ctl.scroll_spd);
}

function scroll_dn_ByID(ctlID) {
	scroll_dn(document.all[ctlID]);
}

function scroll_dn(ctl) {
	ctlID = ctl.id;
	if(ctl.scroll_pos < ctl.scroll_lwr) ctl.scroll_pos += ctl.scroll_stp;
	if(ctl.scroll_pos > ctl.scroll_lwr) ctl.scroll_pos = ctl.scroll_lwr;
	do_scroll(ctl,ctl.scroll_pos);
	ctl.scroll_tim = setTimeout("scroll_dn_ByID('" + ctlID + "')", ctl.scroll_spd);
}

function do_scroll(ctl,pos) {

	if(scroll_iex) ctl.divMen.style.top = pos;
	if(scroll_nav) ctl.divMen.top = pos;
	
	CurrentStartTop = ctl.StartTop + pos;
	ctl.scroll_pos = pos;

	if (pos!= ctl.scroll_lwr && pos!= ctl.scroll_upr && ctl.scroll_flagimg!=1) {
		document.images[ctl.id+"_scroll_down"].src = scroll_img["scroll_down"].src;
		document.images[ctl.id+"_scroll_end"].src = scroll_img["scroll_end"].src;
		document.images[ctl.id+"_scroll_up"].src = scroll_img["scroll_up"].src;
		document.images[ctl.id+"_scroll_start"].src = scroll_img["scroll_start"].src;
		ctl.scroll_flagimg = 1;
	}	

	if (pos== ctl.scroll_lwr && ctl.scroll_flagimg!=2) {
		document.images[ctl.id+"_scroll_down"].src = scroll_img["scroll_down"].src;
		document.images[ctl.id+"_scroll_end"].src = scroll_img["scroll_end"].src;
		document.images[ctl.id+"_scroll_up"].src = scroll_img["scroll_up_d"].src;
		document.images[ctl.id+"_scroll_start"].src = scroll_img["scroll_start_d"].src;
		ctl.scroll_flagimg = 2;
	}
	
	if (pos== ctl.scroll_upr && ctl.scroll_flagimg!=3) {
	
		document.images[ctl.id+"_scroll_down"].src = scroll_img["scroll_down_d"].src;
		document.images[ctl.id+"_scroll_end"].src = scroll_img["scroll_end_d"].src;
		document.images[ctl.id+"_scroll_up"].src = scroll_img["scroll_up"].src;
		document.images[ctl.id+"_scroll_start"].src = scroll_img["scroll_start"].src;
		ctl.scroll_flagimg = 3;
	}
}

function no_scroll(ctl) {
	clearTimeout(ctl.scroll_tim);
}

function scroll_RePos(ctl) {
	try {
		
		ctl.scroll_upr = -(parseInt(ctl.divMen.style.height) - ctl.FWinH + ctl.divBotHeight + ctl.divTopHeight + 2);

		ctl.divTop.style.top=0;ctl.divTop.style.left=0;ctl.divTop.style.width=ctl.FWinW;ctl.divTop.style.height=ctl.divTopHeight;
		ctl.divMid.style.top=ctl.divTopHeight+1;ctl.divMid.style.left=0;ctl.divMid.style.width=ctl.FWinW;ctl.divMid.style.height=document.body.clientHeight-ctl.divTopHeight-ctl.divBotHeight;
		ctl.divBot.style.top=document.body.clientHeight-ctl.divBotHeight;ctl.divBot.style.left=0;ctl.divBot.style.width=ctl.FWinW;ctl.divBot.style.height=ctl.divBotHeight;
		
		if (parseInt(ctl.divMen.style.height) < (ctl.FWinH - ctl.divBotHeight - ctl.divTopHeight - 2)) {

			try {
				ctl.divBot.document.all[ctl.id+"_scroll_botones_1"].style.visibility = "hidden";
				ctl.divBot.document.all[ctl.id+"_scroll_botones_2"].style.visibility = "hidden";
			} catch (e) {}
				do_scroll(ctl,0);
			}
		else {
			try {
				ctl.divBot.document.all[ctl.id+"_scroll_botones_1"].style.visibility = "visible";
				ctl.divBot.document.all[ctl.id+"_scroll_botones_2"].style.visibility = "visible";
			} catch (e) {}
		}
		
	}		
	catch (e) {}
}

function scroll_build(ctl,Lctn) {

			ctl.scroll_pos = 0;   // initial top position
			ctl.scroll_stp = 10;    // step increment size
			ctl.scroll_spd = 50;   // speed of increment
			ctl.scroll_upr = -390;  // upper limiter
			scroll_upr = -390;  // upper limiter
			ctl.scroll_lwr = 0;   // lower limiter
			ctl.scroll_tim;         // timer variable
			ctl.scroll_flagimg = 0; // para evitar redibujados inecesarios

			ctl.divTopHeight = 16;
			ctl.divBotHeight = 80;
			
			ctl.divTop = Lctn.document.createElement("div");
			ctl.divTop.style.position="absolute";
			ctl.divTop.id= ctl.id + "_divTop";
			ctl.divTop.ctl = ctl;
			
			Lctn.document.body.appendChild(ctl.divTop);
			ctl.divTop.style.backgroundColor = ctl.divTop.document.body.bgColor;
			ctl.divTop.style.zIndex = 100;
			ctl.divTop.innerHTML = Lctn.document.all[ctl.id+'_divTopContent'].innerHTML
			ctl.divTop.ctl = ctl;
			
			ctl.divMid=Lctn.document.createElement("div");
			ctl.divMid.style.position="absolute";
			ctl.divMid.id= ctl.id + "_divMid";
			ctl.divMid.ctl = ctl;
			
			ctl.divMen=Lctn.document.createElement("div");
			ctl.divMen.style.position="absolute";
			ctl.divMen.id=ctl.id + "_divMen";
			ctl.divMen.ctl = ctl;
			
			var MbC=Lctn.document.createElement("div");MbC.style.position="absolute";MbC.style.visibility="hidden";
			
			ctl.divMen.appendChild(MbC);
			ctl.divMid.appendChild(ctl.divMen);
			Lctn.document.body.appendChild(ctl.divMid);
			
			ctl.divBot=Lctn.document.createElement("div");
			ctl.divBot.style.position="absolute";
			ctl.divBot.style.backgroundColor = ctl.divTop.document.body.bgColor;
			ctl.divBot.id=ctl.id + "_divBot";
			ctl.divBot.ctl = ctl;
			ctl.divBot.innerHTML = Lctn.document.all[ctl.id+'_divBotContent'].innerHTML;
			Lctn.document.all[ctl.id+'_divBotContent'].innerHTML = "";
			ctl.divBot.style.zIndex = 100;
			Lctn.document.body.appendChild(ctl.divBot);
			
			return MbC;
} 

/*obtiene el control desde el cual se generó cierto evento*/
function getCtl(srcElement) {
		if (typeof(srcElement.ctl) != "undefined") {
			return srcElement.ctl;
		}
		else 
		{
			if(srcElement.parentElement != null) {
				return getCtl(srcElement.parentElement);
			}
			else {
				return null;
			}
		}
}

/*inicializacion de variables para la funcion go*/
function inicializaVars(ctl) {
	ctl.Par=ctl.MenuUsesFrames?parent:window;
	ctl.Doc=ctl.Par.document;
	ctl.Bod=ctl.Doc.body;
	ctl.Trigger=NavYes?ctl.Par:ctl.Bod;
	ctl.FrstCreat=1;
	ctl.RmbrNow=null;
	ctl.RLvl=0;
	ctl.Ldd=0;
	ctl.Crtd=0;
	ctl.AcrssFrms=1;
	ctl.FrstCntnr=null;
	ctl.CurOvr=null;
	ctl.CloseTmr=null;
	ctl.ShwFlg=0;
	ctl.M_StrtTp=ctl.StartTop;
	ctl.M_StrtLft=ctl.StartLeft;
	ctl.StaticPos=0;
	ctl.LftXtra=DomNav?ctl.LeftPadding:0;
	ctl.TpXtra=DomNav?ctl.TopPadding:0;
	ctl.FStr="";
	ctl.M_Hide=Nav4?"hide":"hidden";
	ctl.M_Show=Nav4?"show":"visible";
	ctl.Par=ctl.MenuUsesFrames?parent:window;
	ctl.Doc=ctl.Par.document;
	ctl.Bod=ctl.Doc.body;
	ctl.Trigger=NavYes?ctl.Par:ctl.Bod;
	ctl.OpnTmr=null;
}

function getImg(ctl,Elem,nivel,firstChild,lastChild) {
	var img="";
	
	//if(ctl.MenuTextCentered == "left") {
	//if(ctl.style.align == "left") {
		/*if (ctl.FirstLineHorizontal) {
			if(nivel == 0) {
				img="";
			}
			else if (nivel == 1) {
				img = "";
			}
			else if (lastChild == 1) {
				img = "<img src=\"img/menu/L.GIF\">";
			}
			else if (nivel >= 3 && firstChild == 1) {
				img = "<img src=\"img/menu/T2.GIF\">";
			}
			else {
				img = "<img src=\"img/menu/T.GIF\">";
			}
		}
		else {
		*/
			if(nivel == 1) {
				img="";
			}
			else if ((nivel >= 2) && (firstChild == 1) && (lastChild != 1)) {
				img = "<img src=\"img/menu/T2.GIF\">";
			}
			else if (lastChild == 1) {
				img = "<img src=\"img/menu/L.GIF\">";
			}
			else if (firstChild == 1) {
				img = "<img src=\"img/menu/T.GIF\">";
			}
			else {
				img = "<img src=\"img/menu/T.GIF\">";
		/*	}*/
		}
	//}
	return img;
}

function applyClassNameRecursive(obj,className) {
	a = obj.childNodes;
	obj.className = className;
	if (a) {
		for (i=0; i<a.length; i++) {
			a[i].className = className;
			applyClassNameRecursive(a[i],className);
		}
	}
}
