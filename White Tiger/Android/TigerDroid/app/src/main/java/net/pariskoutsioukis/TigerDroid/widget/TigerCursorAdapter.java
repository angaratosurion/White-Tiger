package net.pariskoutsioukis.TigerDroid.widget;

import android.annotation.SuppressLint;
import android.content.Context;
import android.database.Cursor;
import android.util.Log;
import android.view.View;
import android.widget.*;
import android.content.Context;
import net.pariskoutsioukis.TigerDroid.Tools;


public class TigerCursorAdapter extends SimpleCursorAdapter
{
    protected int[] mTo;

    private int mStringConversionColumn = -1;
    private CursorToStringConverter mCursorToStringConverter;
    private ViewBinder mViewBinder;

    String[] mOriginalFrom,ShownCels;
    protected int[] mFrom;
    @Deprecated
    public TigerCursorAdapter(Context context, int layout, Cursor c, String[] from, int[] to) {
        super(context, layout, c,from,to);
        mTo = to;
        mOriginalFrom = from;



    }

      public void setShownCels(String [] showcels)
      {
          try {
              if (showcels != null) {
                      ShownCels=showcels;
              }
          }
          catch (Exception e)
          {
              Tools.Errorhandling(e);

          }

      }
    public  String [] getShownCels  ()
    {
        try {
            return this.ShownCels;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;

        }
    }

    @SuppressLint("NewApi")
    public TigerCursorAdapter(Context context, int layout, Cursor c, String[] from,
                              int[] to, int flags) {
        super(context, layout, c,from,to, flags);
        mTo = to;
        mOriginalFrom = from;
        findColumns(c, from);

    }

    private void findColumns(Cursor c, String[] from)
    {
        try {
            if (c != null) {
                int i;
                int count = from.length;
                if (mFrom == null || mFrom.length != count) {
                    mFrom = new int[count];
                }
                for (i = 0; i < count; i++) {
                    mFrom[i] = c.getColumnIndexOrThrow(from[i]);
                }
            } else {
                mFrom = null;
            }
        }
        catch(Exception ex)
        {

            Tools.Errorhandling(ex);
        }
    }
    private int [] findShownColumns(Cursor c, String[] from)
    {
        try {
            int[] ap = null;
            if (c != null) {
                int i;
                int count = from.length;
                if (from != null) {
                    ap = new int[count];
                }
                for (i = 0; i < count; i++) {
                    ap[i] = c.getColumnIndexOrThrow(from[i]);
                }
            } else {
                ap = null;
            }
            return ap;
        }
        catch(Exception ex)
        {

            Tools.Errorhandling(ex);

         return null;
        }
    }

    @Override
    public void bindView(View view, Context context, Cursor cursor)
    {
       try {
           final ViewBinder binder = mViewBinder;

           final int count = mTo.length;
           final int[] from = mFrom;
           final int[] to = mTo;
           int[] shcolids;
           int j;
           shcolids = this.findShownColumns(cursor, this.getShownCels());
           for (int i = 0; i < count; i++) {
               final View v = view.findViewById(to[i]);
               if (v != null) {
                   boolean bound = false;
                   if (binder != null) {
                       bound = binder.setViewValue(v, cursor, from[i]);
                   }

                   if (!bound) {
                       String text = cursor.getString(from[i]);

                       if (text == null) {
                           text = "";
                       }
                       if (this.ShownCels != null) {

                           for (j = 0; j < shcolids.length; j++) {
                               if (v.getId() != to[shcolids[j]]) {
                                   v.setVisibility(View.GONE);
                                   if (v instanceof TextView) {
                                       ((TextView) v).setWidth(0);
                                       ((TextView) v).setHeight(0);
                                   }

                               } else {
                                   v.setVisibility(View.VISIBLE);

                               }
                               if (v instanceof TextView) {

                                   if (!((TextView) v).getText().equals(this.getShownCels()[j])) {
                                       v.setVisibility(View.GONE);
                                       if (v instanceof TextView) {
                                           ((TextView) v).setWidth(0);
                                           ((TextView) v).setHeight(0);
                                       }

                                   }
                               }
                           }
                       }
                       if (v instanceof CheckBox) {
                           this.setCheckBoxChecked((CheckBox) v, text);
                       }
                      else if (v instanceof TextView) {
                           setViewText((TextView) v, text);
                       } else if (v instanceof ImageView) {
                           setViewImage((ImageView) v, text);
                       }
                      else {
                           throw new IllegalStateException(v.getClass().getName() + " is not a " +
                                   " view that can be bounds by this SimpleCursorAdapter");
                       }
                   }
               }
           }
       }
       catch(Exception ex)
       {

           Tools.Errorhandling(ex);




       }
    }
     private  void setCheckBoxChecked(CheckBox v,String text)
     {
         try
         {   Boolean b = Boolean.parseBoolean(text);
             v.setChecked(b);


         }
         catch (Exception ex)
         {
             Tools.Errorhandling(ex);
         }

     }

}
